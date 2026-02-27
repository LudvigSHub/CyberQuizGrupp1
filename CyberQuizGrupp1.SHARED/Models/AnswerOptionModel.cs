using System;
using System.Collections.Generic;
using System.Text;
using static CyberQuizGrupp1.SHARED.Models.QuestionModel;

namespace CyberQuizGrupp1.SHARED.Models
{
    public class AnswerOptionModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; } // foreign key
        public QuestionModel Question { get; set; } // navigation property
    }
}
