using CyberQuizGrupp1.SHARED.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.DAL.Repositories.Interfaces
{
    public interface IUserResultRepository
    {
        // Hämtar alla quiz-resultat för en specifik användare
        Task<List<UserResultModel>> GetByUserIdAsync(string userId);

        //sparar ett nytt quiz-resultat i databasen
        Task AddAsync(UserResultModel userResult);

        //hämtar quiz-resultat för subkategori kopplat till en specifik användare 
        Task<List<UserResultModel>> GetByUserIdAndSubCategoryIdAsync(string userId, int subCategoryId);
    }
}
