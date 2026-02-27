using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.SHARED.DTOs
{
    public class UserResultDTO
    {
        public int Id { get; set; }
        public int SubCategoryId { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public bool IsPassed { get; set; }
        public double ScorePercentage { get; set; }
    }
}
