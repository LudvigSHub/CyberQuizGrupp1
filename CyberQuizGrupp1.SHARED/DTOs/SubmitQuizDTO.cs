using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.SHARED.DTOs
{
    public class SubmitQuizDTO
    {
        public int SubCategoryId { get; set; }

        public List<SubmittedAnswerDTO> Answers { get; set; } = new();
    }
}
