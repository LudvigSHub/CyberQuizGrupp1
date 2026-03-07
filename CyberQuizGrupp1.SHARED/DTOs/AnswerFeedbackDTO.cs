using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.SHARED.DTOs
{
    public class AnswerFeedbackDTO
    {
        public bool IsCorrect { get; set; }
        public int CorrectAnswerOptionId { get; set; }
    }
}
