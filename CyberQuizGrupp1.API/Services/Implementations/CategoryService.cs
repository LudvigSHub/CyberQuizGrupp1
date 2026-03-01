using CyberQuizGrupp1.API.Services.Interfaces;
using CyberQuizGrupp1.DAL.Repositories.Interfaces;
using CyberQuizGrupp1.Services.Interfaces;
using CyberQuizGrupp1.SHARED.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberQuizGrupp1.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        //injicera repository för dataåtkomst
        private readonly ICategoryRepository _categoryRepo;

        public CategoryService(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        //hämta alla kategorier från databasen
        public async Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync()
        {
            return await _categoryRepo.GetAllAsync();
        }

        //hämta alla kategorier med deras subkategorier
        public async Task<IEnumerable<CategoryModel>> GetAllCategoriesWithSubCategoriesAsync()
        {
            return await _categoryRepo.GetAllWithSubCategoriesAsync();
        }

        //hämta en specifik kategori baserat på id
        public async Task<CategoryModel?> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepo.GetByIdAsync(id);
        }

        //hämta en kategori med alla dess subkategorier
        public async Task<CategoryModel?> GetCategoryWithSubCategoriesAsync(int id)
        {
            return await _categoryRepo.GetByIdWithSubCategoriesAsync(id);
        }

        //skapa en ny kategori
        public async Task<CategoryModel> CreateCategoryAsync(string name)
        {
            //validera input
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("category name cannot be empty", nameof(name));
            }

            //kontrollera om kategori med samma namn redan finns
            var existing = await _categoryRepo.GetByNameAsync(name);
            if (existing != null)
            {
                throw new InvalidOperationException($"category with name '{name}' already exists");
            }

            //skapa ny kategori
            var category = new CategoryModel { Name = name };
            await _categoryRepo.AddAsync(category);
            await _categoryRepo.SaveChangesAsync();

            return category;
        }

        //uppdatera en befintlig kategori
        public async Task<bool> UpdateCategoryAsync(int id, string newName)
        {
            //validera input
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentException("new name cannot be empty", nameof(newName));
            }

            //hitta kategorin
            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null)
            {
                return false;
            }

            //uppdatera namnet
            category.Name = newName;
            await _categoryRepo.UpdateAsync(category);
            await _categoryRepo.SaveChangesAsync();

            return true;
        }

        //ta bort en kategori
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            //kontrollera om kategorin finns
            if (!await _categoryRepo.ExistsAsync(id))
            {
                return false;
            }

            //ta bort kategorin
            await _categoryRepo.DeleteAsync(id);
            await _categoryRepo.SaveChangesAsync();

            return true;
        }

        //kontrollera om en kategori existerar
        public async Task<bool> CategoryExistsAsync(int id)
        {
            return await _categoryRepo.ExistsAsync(id);
        }
    }
}