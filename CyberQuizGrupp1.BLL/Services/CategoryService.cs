using CyberQuizGrupp1.BLL.Interfaces;
using CyberQuizGrupp1.DAL.Repositories.Interfaces;
using CyberQuizGrupp1.SHARED.DTOs;

namespace CyberQuizGrupp1.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync(string userId)
        {
            var categories = await _categoryRepository.GetAllAsync(userId);

            return categories
                .OrderBy(c => c.Id)
                .Select(c =>
                {
                    var subCategoryDtos = c.SubCategories
                        .OrderBy(sc => sc.Id)
                        .Select(sc =>
                        {
                            bool isCompleted = sc.QuizAttempts.Any(qa =>
                                qa.UserAnswers.Any() &&
                                qa.UserAnswers.Count(ua => ua.IsCorrect) >= Math.Ceiling(qa.UserAnswers.Count * 0.8));

                            return new SubCategoryDTO
                            {
                                Id = sc.Id,
                                Name = sc.Name,
                                IsLocked = sc.IsLocked,
                                IsCompleted = isCompleted,
                                QuestionCount = sc.Questions.Count
                            };
                        })
                        .ToList();

                    return new CategoryDTO
                    {
                        Id = c.Id,
                        CategoryName = c.Name,
                        TotalSubCategories = subCategoryDtos.Count,
                        CompletedSubCategories = subCategoryDtos.Count(sc => sc.IsCompleted),
                        SubCategories = subCategoryDtos
                    };
                })
                .ToList();
        }
    }
}