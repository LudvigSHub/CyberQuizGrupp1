using System.Collections.Generic;
using System.Threading.Tasks;
using CyberQuizGrupp1.SHARED.Models;

namespace CyberQuizGrupp1.Services.Interfaces
{
    public interface ICategoryService
    {
        //hämta alla kategorier
        Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync();

        //hämta alla kategorier med subkategorier
        Task<IEnumerable<CategoryModel>> GetAllCategoriesWithSubCategoriesAsync();

        //hämta en specifik kategori med id
        Task<CategoryModel?> GetCategoryByIdAsync(int id);

        //hämta en kategori med subkategorier
        Task<CategoryModel?> GetCategoryWithSubCategoriesAsync(int id);

        //skapa en ny kategori
        Task<CategoryModel> CreateCategoryAsync(string name);

        //uppdatera en befintlig kategori
        Task<bool> UpdateCategoryAsync(int id, string newName);

        //ta bort en kategori
        Task<bool> DeleteCategoryAsync(int id);

        //kontrollera om kategori existerar
        Task<bool> CategoryExistsAsync(int id);
    }
}