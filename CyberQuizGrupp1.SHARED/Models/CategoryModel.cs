using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.SHARED.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<SubCategoryModel> SubCategories { get; set; } = [];
    }
}
