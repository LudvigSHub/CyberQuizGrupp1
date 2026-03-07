using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.SHARED.DTOs
{
    public class SubmitAnswerDTO
    {
        public Guid AttemptId { get; set; }

        public int QuestionId { get; set; }

        public int SelectedAnswerOptionId { get; set; }
    }
}
