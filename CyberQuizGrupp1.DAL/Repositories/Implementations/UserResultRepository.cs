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

        //sparar ett nytt quiz-resultat i databasen
        public async Task AddAsync(UserResultModel userResult)
        {
            //lägger till resultatet i dbset (märks som "added" i change tracker)
            await _context.UserResults.AddAsync(userResult);
            //sparar ändringarna till databasen (kör insert-kommandot)
            await _context.SaveChangesAsync();
        }
    }
}
