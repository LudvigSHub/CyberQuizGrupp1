using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.SHARED.DTOs
{
    public class SubmittedAnswerDTO
    {
        public int QuestionId { get; set; }
        public int SelectedAnswerId { get; set; }
    }
}
