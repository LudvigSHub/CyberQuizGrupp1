using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.SHARED.Models
{
    public class UserAnswerModel
    {
        public int Id { get; set; }
        public Guid AttemptId { get; set; }
        public QuizAttemptModel Attempt { get; set; }
        public string UserId { get; set; }
        public int QuestionId { get; set; }
        public QuestionModel Question { get; set; }
        public int SelectedAnswerOptionId { get; set; }
        public AnswerOptionModel SelectedAnswerOption { get; set; }
        public bool IsCorrect { get; set; }
        public DateTime AnsweredAt { get; set; }

    }
}
