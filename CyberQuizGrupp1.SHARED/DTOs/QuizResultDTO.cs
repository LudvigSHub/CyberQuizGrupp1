using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.SHARED.DTOs
{
    public class QuizResultDTO
    {
        public int CorrectAnswers { get; set; }
        public int TotalQuestions { get; set; }
        public bool isPassed { get; set; }
        public double scorePercentage { get; set; }
    }
}
