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

            var attempt = new QuizAttemptModel
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                SubCategoryId = subCategory.Id,
                StartedAt = DateTime.UtcNow,
                FinishedAt = null
            };

            await _quizRepository.AddQuizAttemptAsync(attempt);

            var dto = new StartQuizDTO
            {
                AttemptId = attempt.Id,
                SubCategoryId = subCategory.Id,
                SubCategoryName = subCategory.Name,
                TotalQuestions = subCategory.Questions.Count,
                Questions = subCategory.Questions
                .OrderBy(q => q.Id)
                .Select(q => new QuestionDTO
                {
                    Id = q.Id,
                    Text = q.Text,
                    AnswerOptions = q.AnswerOptions
                    .OrderBy(a => a.Id)
                    .Select(a => new AnswerOptionDTO
                    {
                        Id = a.Id,
                        Text = a.Text
                    })
                    .ToList()
                })
                .ToList(),
            };

            return dto;

        }

        public async Task<AnswerFeedbackDTO?> SubmitAnswerAsync(SubmitAnswerDTO dto)
        {
            var attempt = await _quizRepository.GetQuizAttemptByIdAsync(dto.AttemptId);
            if (attempt == null)
                return null;

            var question = await _quizRepository.GetQuestionWithAnswerOptionsAsync(dto.QuestionId);
            if (question == null)
                return null;

            var selectedAnswer = question.AnswerOptions
                .FirstOrDefault(a => a.Id == dto.SelectedAnswerOptionId);

            if (selectedAnswer == null)
                return null;

            var correctAnswer = question.AnswerOptions
                .FirstOrDefault(a => a.IsCorrect);

            if (correctAnswer == null)
                return null;

            var userAnswer = new UserAnswerModel
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
                IsCorrect = selectedAnswer.IsCorrect,
                CorrectAnswerOptionId = correctAnswer.Id
            };
        }

        public async Task<QuizResultDTO?> FinishQuizAsync(FinishQuizDTO dto)
        {
            var attempt = await _quizRepository.GetQuizAttemptByIdAsync(dto.AttemptId);
            if (attempt == null)
                return null;

            var subCategory = await _quizRepository.GetQuizDataBySubCategoryIdAsync(attempt.SubCategoryId);
            if (subCategory == null)
                return null;

            var userAnswers = await _quizRepository.GetUserAnswersByAttemptIdAsync(dto.AttemptId);

            int correctAnswers = userAnswers.Count(a => a.IsCorrect);
            int totalQuestions = subCategory.Questions.Count;

            double scorePercentage = totalQuestions == 0
                ? 0
                : (double)correctAnswers / totalQuestions * 100.0;

            bool isPassed = scorePercentage >= 80.0;

            var userResult = new UserResultModel
            {
                UserId = attempt.UserId,
                SubCategoryId = attempt.SubCategoryId,
                TotalQuestions = totalQuestions,
                CorrectAnswers = correctAnswers
            };

            await _userResultRepository.AddAsync(userResult);
            await _quizRepository.MarkQuizAttemptAsFinishedAsync(dto.AttemptId);

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
