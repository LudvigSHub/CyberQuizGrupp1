using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.SHARED.DTOs
{
    public class StartQuizDTO
    {
        public Guid AttemptId { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; } = string.Empty;
        public int TotalQuestions { get; set; }
        public List<QuestionDTO> Questions { get; set; } = [];
        
    }
}
