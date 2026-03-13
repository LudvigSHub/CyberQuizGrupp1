using System.Text;
using CyberQuizGrupp1.BLL.Interfaces;
using CyberQuizGrupp1.DAL.Repositories.Interfaces;
using CyberQuizGrupp1.SHARED.DTOs;
using CyberQuizGrupp1.SHARED.Models;

namespace CyberQuizGrupp1.BLL.Services
{
    public class CoachingService : ICoachingService
    {
        private const double RequiredScorePercentage = 75.0;

        private readonly IUserResultRepository _userResultRepository;
        private readonly IQuizRepository _quizRepository;
        private readonly IAiCoachClient _aiCoachClient;

        public CoachingService(
            IUserResultRepository userResultRepository,
            IQuizRepository quizRepository,
            IAiCoachClient aiCoachClient)
        {
            _userResultRepository = userResultRepository;
            _quizRepository = quizRepository;
            _aiCoachClient = aiCoachClient;
        }

        // Hämta alla subcategories som användaren gjort men ej klarat
        public async Task<List<CoachingItemDTO>> GetFailedSubCategoriesAsync(string userId)
        {
            var userResults = await _userResultRepository.GetByUserIdAsync(userId);

            var failedSubCategoryIds = userResults
                .Where(r => r.TotalQuestions > 0)
                .GroupBy(r => r.SubCategoryId)
                .Where(g => g.Max(r => (double)r.CorrectAnswers / r.TotalQuestions * 100.0) < RequiredScorePercentage)
                .Select(g => g.Key)
                .ToList();

            var result = new List<CoachingItemDTO>();

            foreach (var subCategoryId in failedSubCategoryIds)
            {
                var subCategory = await _quizRepository.GetQuizDataBySubCategoryIdAsync(subCategoryId);

                if (subCategory == null)
                    continue;

                result.Add(new CoachingItemDTO
                {
                    SubCategoryId = subCategory.Id,
                    SubCategoryName = subCategory.Name
                });
            }

            return result;
        }

        // Hämta coaching för en specifik subcategory baserat på användarens svar
        public async Task<CoachingResponseDTO?> GetCoachingAsync(int subCategoryId, string userId)
        {
            var subCategory = await _quizRepository.GetQuizDataBySubCategoryIdAsync(subCategoryId);
            if (subCategory == null)
                return null;

            var userResults = await _userResultRepository.GetByUserIdAndSubCategoryIdAsync(userId, subCategoryId);
            if (!userResults.Any())
                return null;

            var userAnswers = await _quizRepository.GetUserAnswersByUserAndSubCategoryAsync(userId, subCategoryId);
            if (!userAnswers.Any())
                return null;

            // hämta upp alla frågor användaren svarat på i subkategorin och dela upp i rätt och fel
            var correctAnswers = userAnswers.Where(a => a.IsCorrect).ToList();
            var incorrectAnswers = userAnswers.Where(a => !a.IsCorrect).ToList();

            // skapa underlag för styrkor och svagheter för AI 
            var strengthEvidence = BuildStrengths(correctAnswers);
            var weaknessEvidence = BuildWeaknesses(incorrectAnswers);

            var prompt = BuildPrompt(subCategory.Name, strengthEvidence, weaknessEvidence);

            // skicka promten till AI och hämta coachingtext
            var aiText = await _aiCoachClient.GetCoachingTextAsync(prompt);

            // gör om AI-svaret till en strukturerad DTO
            var parsedResponse = ParseAiResponse(aiText);

            return new CoachingResponseDTO
            {
                SubCategoryId = subCategory.Id,
                SubCategoryName = subCategory.Name,
                StrengthSummary = parsedResponse.StrengthSummary,
                WeaknessSummary = parsedResponse.WeaknessSummary,
                CoachText = parsedResponse.CoachText
            };
        }

        private string BuildStrengths(List<UserAnswerModel> correctAnswers)
        {
            if (!correctAnswers.Any())
            {
                return "Inga tydliga styrkor identifierade.";
            }

            var correctQuestionTexts = correctAnswers
                .Select(a => a.Question.Text)
                .Where(text => !string.IsNullOrWhiteSpace(text))
                .Distinct()
                .Take(3)
                .ToList();

            return string.Join(", ", correctQuestionTexts);
        }

        private string BuildWeaknesses(List<UserAnswerModel> incorrectAnswers)
        {
            if (!incorrectAnswers.Any())
            {
                return "Inga tydliga svagheter identifierade.";
            }

            var incorrectQuestionTexts = incorrectAnswers
                .Select(a => a.Question.Text)
                .Where(text => !string.IsNullOrWhiteSpace(text))
                .Distinct()
                .Take(3)
                .ToList();

            return string.Join(", ", incorrectQuestionTexts);
        }

        // skapa promt för AI med 1, styrkor, 2. svagheter, 3. rekommendation
        private string BuildPrompt(string subCategoryName, string strengths, string weaknesses)
        {
            var sb = new StringBuilder();

            sb.AppendLine("Du är en saklig AI-coach för ett cyber security-quiz.");
            sb.AppendLine("Svara på svenska.");
            sb.AppendLine("Sammanfatta kort i egna ord.");
            sb.AppendLine("Undvik emojis, markdown, rubriker och utfyllnad.");
            sb.AppendLine();
            sb.AppendLine($"Subkategori: {subCategoryName}");
            sb.AppendLine($"Styrkor underlag: {strengths}");
            sb.AppendLine($"Svagheter underlag: {weaknesses}");
            sb.AppendLine();
            sb.AppendLine("Skriv styrkor i stil med 'Du förstår dig på, ...'");
            sb.AppendLine("Skriv svagheter i stil med 'Dina svagheter är, ...'");
            sb.AppendLine("Skriv rekommendation i stil med 'Jag rekommenderar, ...'");
            sb.AppendLine("Svara i exakt tre rader och inget annat.");
            sb.AppendLine("1. Namnge styrkor kortfattat.");
            sb.AppendLine("2. Namnge svagheter kortfattat.");
            sb.AppendLine("3. Rekommendation om hur användaren bör träna.");

            return sb.ToString();
        }

        // gör om responset så att det kan användas i UI efter styrkor, svagheter och coachingtext har plockats ut
        private CoachingResponseDTO ParseAiResponse(string aiText)
        {
            var response = new CoachingResponseDTO();

            if (string.IsNullOrWhiteSpace(aiText))
            {
                response.StrengthSummary = "Kunde inte generera styrkor.";
                response.WeaknessSummary = "Kunde inte generera svagheter.";
                response.CoachText = "Kunde inte generera coaching.";
                return response;
            }

            var lines = aiText
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Replace("**", "").Trim())
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .ToList();

            foreach (var line in lines)
            {
                if (line.StartsWith("1."))
                {
                    response.StrengthSummary = line.Substring(2).Trim();
                }
                else if (line.StartsWith("2."))
                {
                    response.WeaknessSummary = line.Substring(2).Trim();
                }
                else if (line.StartsWith("3."))
                {
                    response.CoachText = line.Substring(2).Trim();
                }
            }

            if (string.IsNullOrWhiteSpace(response.StrengthSummary) && lines.Count > 0)
                response.StrengthSummary = lines[0];

            if (string.IsNullOrWhiteSpace(response.WeaknessSummary) && lines.Count > 1)
                response.WeaknessSummary = lines[1];

            if (string.IsNullOrWhiteSpace(response.CoachText) && lines.Count > 2)
                response.CoachText = lines[2];

            if (string.IsNullOrWhiteSpace(response.StrengthSummary))
                response.StrengthSummary = "Ingen tydlig styrkesammanfattning genererades.";

            if (string.IsNullOrWhiteSpace(response.WeaknessSummary))
                response.WeaknessSummary = "Ingen tydlig svaghetssammanfattning genererades.";

            if (string.IsNullOrWhiteSpace(response.CoachText))
                response.CoachText = "Ingen tydlig coaching genererades.";

            return response;
        }
    }
}

