using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return categories
                .OrderBy(c => c.Id)
                .Select(c => new CategoryDTO
                {
                    Id = c.Id,
                    CategoryName = c.Name,
                    
                })
                .ToList();
        }
    }
}
