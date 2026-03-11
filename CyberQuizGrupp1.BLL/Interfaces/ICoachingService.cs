using System;
using System.Collections.Generic;
using System.Text;
using CyberQuizGrupp1.SHARED.DTOs;

namespace CyberQuizGrupp1.BLL.Interfaces
{
    public interface ICoachingService
    {
        Task<List<CoachingItemDTO>> GetFailedSubCategoriesAsync(string userId);

        Task<CoachingResponseDTO?> GetCoachingAsync(int subCategoryId, string userId);
    }
}
