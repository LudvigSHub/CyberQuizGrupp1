using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CyberQuizGrupp1.DAL.Data;
using CyberQuizGrupp1.DAL.Repositories.Interfaces;
using CyberQuizGrupp1.SHARED.Models;
using Microsoft.EntityFrameworkCore;

namespace CyberQuizGrupp1.DAL.Repositories.Implementations
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        //injicerad databas-kontext för att prata med databasen
        private readonly AppDbContext _context;

        //konstruktor som tar emot appdbcontext via dependency injection
        public SubCategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        //hämtar subkategorier för en kategori och inkluderar questions (för questioncount i bll)
        public async Task<List<SubCategoryModel>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.SubCategories
                .Include(sc => sc.Questions) //inkludera questions så bll kan räkna antal
                .Where(sc => sc.CategoryId == categoryId)
                .OrderBy(sc => sc.Id)
                .ToListAsync();
        }

        //hämtar alla subkategorier
        public async Task<List<SubCategoryModel>> GetAllAsync()
        {
            return await _context.SubCategories //hämtar från tabellen SubCategories
                .OrderBy(sc => sc.Id)           //sorterar efter Id
                .ToListAsync();                 //gör om till lista 
        }
    }
}