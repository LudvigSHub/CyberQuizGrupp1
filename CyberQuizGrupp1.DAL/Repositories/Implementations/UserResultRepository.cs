using CyberQuizGrupp1.DAL.Data;
using CyberQuizGrupp1.DAL.Repositories.Interfaces;
using CyberQuizGrupp1.SHARED.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.DAL.Repositories.Implementations
{
    public class UserResultRepository : IUserResultRepository
    {
        private readonly AppDbContext _context;

        public UserResultRepository(AppDbContext context)
        {
            _context = context;
        }

        // Hämtar alla resultat för en användare (används i SubCategoryService)
        public async Task<List<UserResultModel>> GetByUserIdAsync(string userId)
        {
            return await _context.UserResults
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }
    }
}
