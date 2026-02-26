using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.SHARED.Models
{
    public class UserResultModel
    {
        public int Id { get; set; }
        public string UserId { get; set; } // koppling till Identity användaren
        public int SubCategoryId { get; set; } // foreign key
        public SubCategoryModel SubCategory { get; set; } // navigation property
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public double ScorePercentage => TotalQuestions == 0 ? 0 : (double)CorrectAnswers / TotalQuestions * 100; // räknas ut automatiskt
        public DateTime CompletedAt { get; set; } = DateTime.Now;
    }
}
