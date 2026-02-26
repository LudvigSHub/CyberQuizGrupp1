using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.SHARED.Models
{
    public class SubCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsLocked { get; set; } = true;
        public int CategoryId { get; set; } // foreign key
        public CategoryModel Category { get; set; } // navigation property
        public List<QuestionModel> Questions { get; set; } = [];
    }
}
