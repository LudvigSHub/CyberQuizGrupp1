using System;
using System.Collections.Generic;
using System.Text;
using CyberQuizGrupp1.SHARED.DTOs;

namespace CyberQuizGrupp1.BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllCategoriesAsync(string userId);
    }
}
