using System.Collections.Generic;
using System.Threading.Tasks;
using CyberQuizGrupp1.DAL.Data;
using CyberQuizGrupp1.DAL.Repositories.Interfaces;
using CyberQuizGrupp1.SHARED.DTOs;
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
        public async Task<List<CategoryModel>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
