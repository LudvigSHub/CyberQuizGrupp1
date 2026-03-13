using System;
using System.Collections.Generic;
using System.Text;
using CyberQuizGrupp1.BLL.Interfaces;
using CyberQuizGrupp1.DAL.Repositories.Implementations;
using CyberQuizGrupp1.DAL.Repositories.Interfaces;
using CyberQuizGrupp1.SHARED.DTOs;

namespace CyberQuizGrupp1.BLL.Services
{
    // Hämta user progress genom att räkna vilka subcategories som är avklarade
    // räkna klarade subcategories av totala och gör om till procent
    // returnera för att använda i progressbar i UI
    public class ProgressService : IProgressService
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IUserResultRepository _userResultRepository;

        public ProgressService(ISubCategoryRepository subCategoryRepository, IUserResultRepository userResultRepository)
        {
            _subCategoryRepository = subCategoryRepository;
            _userResultRepository = userResultRepository;
        }

        public async Task<UserProgressDTO> GetUserProgressAsync(string userId)
        {

            var subCategories = await _subCategoryRepository.GetAllAsync();

            var userResults = await _userResultRepository.GetByUserIdAsync(userId);

            int totalSubCategories = subCategories.Count;

            var bestScoreBySubCategoryId = userResults
        .Where(r => r.TotalQuestions > 0)
        .GroupBy(r => r.SubCategoryId)
        .ToDictionary(
            g => g.Key,
            g => g.Max(r => (double)r.CorrectAnswers / r.TotalQuestions * 100.0)
        );

            int completed = subCategories.Count(sc =>
        bestScoreBySubCategoryId.TryGetValue(sc.Id, out var score) &&
        score >= 75.0);

            double progressPercentage = totalSubCategories == 0 ? 0 : (double)completed / totalSubCategories * 100;

            return new UserProgressDTO
            {
                TotalSubCategories = totalSubCategories,
                CompletedSubCategories = completed,
                ProgressPercentage = progressPercentage
            };
        }

    }
}

