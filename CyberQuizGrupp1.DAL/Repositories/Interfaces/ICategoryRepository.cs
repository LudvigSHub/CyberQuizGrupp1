using System.Collections.Generic;
using System.Threading.Tasks;
using CyberQuizGrupp1.SHARED.DTOs;
using CyberQuizGrupp1.SHARED.Models;

namespace CyberQuizGrupp1.DAL.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        //hämta alla kategorier från databasen
        Task<List<CategoryModel>> GetAllAsync(string userId);
    }
}
