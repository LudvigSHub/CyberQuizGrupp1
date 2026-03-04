using System.Collections.Generic;
using System.Threading.Tasks;
using CyberQuizGrupp1.SHARED.DTOs;
using CyberQuizGrupp1.SHARED.Models;

namespace CyberQuizGrupp1.DAL.Repositories.Interfaces
{
    public interface ISubCategoryRepository
    {
        // hämta alla subkategorier för en kategori
        Task<List<SubCategoryModel>> GetByCategoryIdAsync(int categoryId);
    }
}