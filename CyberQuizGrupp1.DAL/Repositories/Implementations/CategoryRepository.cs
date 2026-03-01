using System.Collections.Generic;
using System.Threading.Tasks;
using CyberQuizGrupp1.DAL.Data;
using CyberQuizGrupp1.DAL.Repositories.Interfaces;
using CyberQuizGrupp1.SHARED.Models;
using Microsoft.EntityFrameworkCore;

namespace CyberQuizGrupp1.DAL.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        //injicerad databas-kontext för att prata med databasen
        private readonly AppDbContext _context;

        //konstruktor som tar emot appdbcontext via dependency injection
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        //hämtar alla kategorier som en lista
        public async Task<IEnumerable<CategoryModel>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        //hittar en kategori baserat på primary key (id)
        public async Task<CategoryModel?> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        //lägger till en ny kategori i databasen (men sparar inte ännu - kräver savechangesasync)
        public async Task AddAsync(CategoryModel category)
        {
            await _context.Categories.AddAsync(category);
        }

        //markerar en kategori som uppdaterad
        public async Task UpdateAsync(CategoryModel category)
        {
            _context.Categories.Update(category);
            await Task.CompletedTask; 
        }

        //tar bort en kategori från databasen baserat på id
        public async Task DeleteAsync(int id)
        {
            var category = await GetByIdAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
        }

        //kontrollerar om en kategori med specifikt id finns i databasen
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }

        //sparar alla ändringar till databasen
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }


        //hämtar alla kategorier och inkluderar deras subkategorier
        public async Task<IEnumerable<CategoryModel>> GetAllWithSubCategoriesAsync()
        {
            return await _context.Categories
                .Include(c => c.SubCategories)
                .ToListAsync();
        }

        //hämtar en specifik kategori med alla dess subkategorier
        public async Task<CategoryModel?> GetByIdWithSubCategoriesAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.SubCategories)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        //hittar en kategori baserat på namn
        public async Task<CategoryModel?> GetByNameAsync(string name)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}
