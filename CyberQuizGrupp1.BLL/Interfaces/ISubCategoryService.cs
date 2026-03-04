using System;
using System.Collections.Generic;
using System.Text;
using CyberQuizGrupp1.SHARED.DTOs;

namespace CyberQuizGrupp1.BLL.Interfaces
{
    public interface ISubCategoryService
    {
        public Task<List<SubCategoryDTO>> GetSubCategoriesByCategoryAsync(int categoryId, string userId);
    }
}
