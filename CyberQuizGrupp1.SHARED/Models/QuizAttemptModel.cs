using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.SHARED.Models
{
    public class QuizAttemptModel 
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public int SubCategoryId { get; set; }
        public SubCategoryModel SubCategory { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public List<UserAnswerModel> UserAnswers { get; set; } = new ();

     }
}
