using System;
using System.Collections.Generic;
using System.Text;
using CyberQuizGrupp1.BLL.Interfaces;
using CyberQuizGrupp1.DAL.Repositories.Interfaces;
using CyberQuizGrupp1.SHARED.DTOs;
using CyberQuizGrupp1.SHARED.Models;

namespace CyberQuizGrupp1.BLL.Services
{
    public class QuizService : IQuizService
    {
        // Dependency injection av repositories som behövs för att hämta data och spara resultat
        private readonly IQuizRepository _quizRepository;
        private readonly IUserResultRepository _userResultRepository;


        public QuizService(IQuizRepository quizRepository, IUserResultRepository userResultRepository)
        {
            _quizRepository = quizRepository;
            _userResultRepository = userResultRepository;
        }

        public async Task<StartQuizDTO?> StartQuizAsync(int subCategoryId, string userId)
        {
            var subCategory = await _quizRepository.GetQuizDataBySubCategoryIdAsync(subCategoryId);
            if (subCategory is null)
            {
                return null;
            }

            // Skapa en ny quizförsök/quiz omgång och spara det i databasen
            var attempt = new QuizAttemptModel
            {
                // Guid används för att skapa ett unikt värde för att identifiera varje quizförsök, vilket gör det enkelt att spåra användarens framsteg och svar under quizet
                Id = Guid.NewGuid(),
                UserId = userId,
                SubCategoryId = subCategory.Id,
                StartedAt = DateTime.UtcNow,
                FinishedAt = null
            };

            await _quizRepository.AddQuizAttemptAsync(attempt);

            // Mappa om data från subkategorin och frågorna till en StartQuizDTO som skickas tillbaka till frontend så att quizet kan startas
            var dto = new StartQuizDTO
            {
                AttemptId = attempt.Id,
                SubCategoryId = subCategory.Id,
                SubCategoryName = subCategory.Name,
                TotalQuestions = subCategory.Questions.Count, // Räkna hur många frågor som finns i subkategorin och inkludera det i DTO:n så att frontend kan visa det för användaren
                Questions = subCategory.Questions
                .OrderBy(q => q.Id) // Sortera frågorna i bestämd ordning efter Id
                .Select(q => new QuestionDTO // Mappa varje fråga till en QuestionDTO
                {
                    Id = q.Id,
                    Text = q.Text,
                    AnswerOptions = q.AnswerOptions // för varje fråga, mappa svarsalternatigen till en lista av Answers
                    .OrderBy(a => Guid.NewGuid()) // Slumpa svarsalternativen så att de inte alltid visas i samma ordning
                    .Select(a => new AnswerOptionDTO // Mappa varje svarsalternativ till en AnswerOptionDTO
                    {
                        Id = a.Id,
                        Text = a.Text
                    })
                    .ToList() // Konvertera den mappade listan av svarsalternativ till en List<AnswerOptionDTO>
                })
                .ToList(), // Konvertera den mappade listan av frågor till en List<QuestionDTO>
            };

            return dto; // Returnera den skapade StartQuizDTO:n så att UI kan använda den för att starta quizet

        }

        public async Task<AnswerFeedbackDTO?> SubmitAnswerAsync(SubmitAnswerDTO dto)
        {
            var attempt = await _quizRepository.GetQuizAttemptByIdAsync(dto.AttemptId); // Hämta quizförsöket med DTO:n som innehåller AttemptId
            if (attempt == null)
                return null;

            var question = await _quizRepository.GetQuestionWithAnswerOptionsAsync(dto.QuestionId); // Hämta frågan med tillhörande svarsalternativ
            if (question == null)
                return null;

            var selectedAnswer = question.AnswerOptions // Hämta det valda svarsalternativet baserat på SelectedAnswerOptionId i DTO:n
                .FirstOrDefault(a => a.Id == dto.SelectedAnswerOptionId);

            if (selectedAnswer == null)
                return null;

            var correctAnswer = question.AnswerOptions // Hämta det korrekta svarsalternativet för frågan
                .FirstOrDefault(a => a.IsCorrect);

            if (correctAnswer == null)
                return null;

            var userAnswer = new UserAnswerModel // Skapa en UserAnswerModel som representerar användarens svar och spara det i databasen
            {
                AttemptId = dto.AttemptId,
                UserId = attempt.UserId,
                QuestionId = dto.QuestionId,
                SelectedAnswerOptionId = dto.SelectedAnswerOptionId,
                IsCorrect = selectedAnswer.IsCorrect,
                AnsweredAt = DateTime.UtcNow
            };

            await _quizRepository.AddUserAnswerAsync(userAnswer);

            return new AnswerFeedbackDTO
            {
                IsCorrect = selectedAnswer.IsCorrect, // returnera om svaret var korrekt eller inte
                CorrectAnswerOptionId = correctAnswer.Id // returnera Id för det korrekta svaret ifall UI vill visa det
            };
        }

        public async Task<QuizResultDTO?> FinishQuizAsync(FinishQuizDTO dto)
        {
            var attempt = await _quizRepository.GetQuizAttemptByIdAsync(dto.AttemptId); // hämta quizförsöket
            if (attempt == null)
                return null;

            var subCategory = await _quizRepository.GetQuizDataBySubCategoryIdAsync(attempt.SubCategoryId); // Hämta Subcategory för quizförsöket för att räkna frågor
            if (subCategory == null)
                return null;

            var userAnswers = await _quizRepository.GetUserAnswersByAttemptIdAsync(dto.AttemptId); // Hämta alla svar som tillhör quizförsöket

            // Jämföra användarens svar med de korrekta svaren för att räkna hur många som var rätt och hur många frågor det totalt var i quizet
            int correctAnswers = userAnswers.Count(a => a.IsCorrect);
            int totalQuestions = subCategory.Questions.Count;

            // räkna ut procenten av rätta svar och avgöra om användaren klarade quizet (75% eller mer rätt)
            double scorePercentage = totalQuestions == 0
                ? 0
                : (double)correctAnswers / totalQuestions * 100.0;

            bool isPassed = scorePercentage >= 75.0;

            // Spara resultatet i UserResultModel
            var userResult = new UserResultModel
            {
                UserId = attempt.UserId,
                SubCategoryId = attempt.SubCategoryId,
                TotalQuestions = totalQuestions,
                CorrectAnswers = correctAnswers
            };
            // Spara användarens resultat i databasen och markera quizförsöket som avslutat
            await _userResultRepository.AddAsync(userResult);
            await _quizRepository.MarkQuizAttemptAsFinishedAsync(dto.AttemptId);

            // Returnera resultatet i en QuizResultDTO som UI kan använda för att visa resultatet för användaren
            return new QuizResultDTO
            {
                CorrectAnswers = correctAnswers,
                TotalQuestions = totalQuestions,
                isPassed = isPassed,
                scorePercentage = scorePercentage
            };
        }
    }
}
