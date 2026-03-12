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

        //hämtar alla quiz-resultat för en specifik användare och subkategori
        //används för att analysera användarens prestanda i en specifik subkategori för coaching
        public async Task<List<UserResultModel>> GetByUserIdAndSubCategoryIdAsync(string userId, int subCategoryId)
        {
            //hämtar alla resultat som matchar både userid och subkategoryid
            return await _context.UserResults
                .Where(r => r.UserId == userId && r.SubCategoryId == subCategoryId) //filtrera på både användare och subkategori
                .ToListAsync(); //konvertera till lista
        }
    }
}
