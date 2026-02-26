using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.SHARED.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int SubCategoryId { get; set; } // foreign key
        public SubCategoryModel SubCategory { get; set; } // navigation property
        public List<AnswerOptionModel> AnswerOptions { get; set; } = [];

    }
}
