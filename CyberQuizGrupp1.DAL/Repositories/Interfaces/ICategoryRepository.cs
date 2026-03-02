using System.Collections.Generic;
using System.Threading.Tasks;
using CyberQuizGrupp1.SHARED.DTOs;
using CyberQuizGrupp1.SHARED.Models;

namespace CyberQuizGrupp1.DAL.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        //hämta alla kategorier från databasen
        Task<List<CategoryModel>> GetAllAsync();

        ////hämta en specifik kategori baserat på id
        //Task<CategoryModel?> GetByIdAsync(int id);

        ////lägg till en ny kategori i databasen
        //Task AddAsync(CategoryModel category);

        ////uppdatera en befintlig kategori
        //Task UpdateAsync(CategoryModel category);

        ////ta bort en kategori baserat på id
        //Task DeleteAsync(int id);

        ////kontrollera om en kategori med specifikt id existerar
        //Task<bool> ExistsAsync(int id);

        ////spara alla ändringar till databasen
        //Task<int> SaveChangesAsync();

        ////hämta alla kategorier med deras subkategorier 
        //Task<IEnumerable<CategoryModel>> GetAllWithSubCategoriesAsync();

        ////hämta en specifik kategori med dess subkategorier
        //Task<CategoryModel?> GetByIdWithSubCategoriesAsync(int id);

        ////hämta en kategori baserat på namn
        //Task<CategoryModel?> GetByNameAsync(string name);
    }
}
