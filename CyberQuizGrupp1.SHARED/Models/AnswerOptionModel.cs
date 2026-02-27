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
        public bool IsCorrect { get; set; } //bool för att sätta om svaret är rätt eller fel
        public int QuestionId { get; set; } // foreign key som kopplar ihop och berättar vilken Question (parent) som AnswerOptions (child) tillhör 
        public QuestionModel Question { get; set; } // navigation property som låter oss navigera upp till parent (QuestionModel) från child (AnswerOptions), many to one, många AnswerOptions kan tillhöra en QuestionModel

    }
}
