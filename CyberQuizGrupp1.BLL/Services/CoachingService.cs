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
                var subCategory = await _quizRepository.GetQuizDataBySubCategoryIdAsync
                    (subCategoryId);

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

            var correctAnswers = userAnswers.Where(a => a.IsCorrect).ToList();
            var incorrectAnswers = userAnswers.Where(a => !a.IsCorrect).ToList();

            var strengths = BuildStrengths(correctAnswers);
            var weaknesses = BuildWeaknesses(incorrectAnswers);

            var prompt = BuildPrompt(subCategory.Name, strengths, weaknesses);

            var aiText = await _aiCoachClient.GetCoachingTextAsync(prompt);

            return new CoachingResponseDTO
            {
                SubCategoryId = subCategory.Id,
                SubCategoryName = subCategory.Name,
                StrengthSummary = strengths,
                WeaknessSummary = weaknesses,
                CoachText = aiText
            };
        }

        private string BuildStrengths(List<UserAnswerModel> correctAnswers)
        {
            if (!correctAnswers.Any())
            {
                return "Användaren har ännu inga tydliga styrkor i denna subkategori.";
            }

            var correctQuestionTexts = correctAnswers
                .Select(a => a.Question.Text)
                .Distinct()
                .Take(3)
                .ToList();

            return "Användaren har ofta svarat rätt på frågor som handlar om: " +
                   string.Join(", ", correctQuestionTexts);
        }

        private string BuildWeaknesses(List<UserAnswerModel> incorrectAnswers)
        {
            if (!incorrectAnswers.Any())
            {
                return "Användaren visar inga tydliga svagheter i denna subkategori.";
            }

            var incorrectQuestionTexts = incorrectAnswers
                .Select(a => a.Question.Text)
                .Distinct()
                .Take(3)
                .ToList();

            return "Användaren har ofta svarat fel på frågor som handlar om: " +
                   string.Join(", ", incorrectQuestionTexts);
        }

        private string BuildPrompt(string subCategoryName, string strengths, string weaknesses)
        {
            var sb = new StringBuilder();

            sb.AppendLine("Du är en pedagogisk AI-coach för ett cyber security-quiz.");
            sb.AppendLine("Svara på svenska.");
            sb.AppendLine("Var tydlig, uppmuntrande, konkret och ganska kortfattad.");
            sb.AppendLine();
            sb.AppendLine($"Subkategori: {subCategoryName}");
            sb.AppendLine($"Styrkor: {strengths}");
            sb.AppendLine($"Svagheter: {weaknesses}");
            sb.AppendLine();
            sb.AppendLine("Skriv:");
            sb.AppendLine("1. En kort sammanfattning av användarens styrkor");
            sb.AppendLine("2. En kort sammanfattning av användarens svagheter");
            sb.AppendLine("3. Konkreta rekommendationer på vad användaren ska träna vidare på");
            sb.AppendLine("Svaret ska passa att visa direkt i ett quiz-UI.");

            return sb.ToString();
        }
    }
}

