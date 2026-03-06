using System;
using System.Collections.Generic;
using System.Text;
using CyberQuizGrupp1.BLL.Interfaces;
using CyberQuizGrupp1.SHARED.DTOs;

namespace CyberQuizGrupp1.BLL.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;

        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<StartQuizDTO> GetQuizBySubCategoryAsync(int subCategoryId)
        {
            var subCategory = await _quizRepository.GetQuizBySubCategoryIdAsync(subCategoryId);
            if (subCategory == 0)
            {
                return null;
            }

            var dto = new StartQuizDTO
            {
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
    }
}
