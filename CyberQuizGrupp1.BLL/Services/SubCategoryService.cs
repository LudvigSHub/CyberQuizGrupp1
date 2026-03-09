using System;
using System.Collections.Generic;
using System.Text;
using CyberQuizGrupp1.BLL.Interfaces;
using CyberQuizGrupp1.SHARED.DTOs;
using CyberQuizGrupp1.SHARED.Models;
using CyberQuizGrupp1.DAL.Repositories.Interfaces;

namespace CyberQuizGrupp1.BLL.Services
{
    public class SubCategoryService : ISubCategoryService
    {

        private const double RequiredScorePercentage = 75.0;

        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IUserResultRepository _userResultRepository;

        public SubCategoryService(
            ISubCategoryRepository subCategoryRepository,
            IUserResultRepository userResultRepository)
        {
            _subCategoryRepository = subCategoryRepository;
            _userResultRepository = userResultRepository;
        }

        public async Task<List<SubCategoryDTO>> GetSubCategoriesByCategoryAsync(int categoryId, string userId)
        {
            // 1) Hämta subkategorier (se till att repo inkluderar Questions så Count funkar)
            var subCategories = await _subCategoryRepository.GetByCategoryIdAsync(categoryId);

            var orderedSubCategories = subCategories
                .OrderBy(sc => sc.Id) // seed-ordning (ni kör fasta Id:n)
                .ToList();

            // 2) Hämta userns resultat (du kan också göra en repo-metod som filtrerar på dessa subCategoryIds)
            var userResults = await _userResultRepository.GetByUserIdAsync(userId);

            // 3) Räkna ut BEST score% per SubCategoryId
            var bestScoreBySubCategoryId = userResults
                .Where(r => r.TotalQuestions > 0) // skydd mot div/0
                .GroupBy(r => r.SubCategoryId)
                .ToDictionary(
                    g => g.Key,
                    g => g.Max(r => (double)r.CorrectAnswers / r.TotalQuestions * 100.0)
                );

            // 4) Skapa DTO-lista med IsCompleted (men IsLocked sätter vi efteråt)
            var dtoList = orderedSubCategories.Select(sc =>
            {
                var questionCount = sc.Questions?.Count ?? 0;

                var bestScore = bestScoreBySubCategoryId.TryGetValue(sc.Id, out var score)
                    ? score
                    : 0.0;

                var isCompleted = bestScore >= RequiredScorePercentage;

                return new SubCategoryDTO
                {
                    Id = sc.Id,
                    Name = sc.Name,
                    QuestionCount = questionCount,
                    IsCompleted = isCompleted,
                    IsLocked = true // default, sätts i nästa steg
                };
            }).ToList();

            // 5) Låslogik: första alltid upplåst, resten beror på föregående
            for (int i = 0; i < dtoList.Count; i++)
            {
                if (i == 0)
                {
                    dtoList[i].IsLocked = false;
                }
                else
                {
                    dtoList[i].IsLocked = !dtoList[i - 1].IsCompleted;
                }
            }

            return dtoList;
        }
    }
}
