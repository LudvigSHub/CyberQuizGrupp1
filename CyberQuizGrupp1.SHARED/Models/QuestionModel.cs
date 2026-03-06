using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.SHARED.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int SubCategoryId { get; set; } // foreign key som kopplar ihop och berättar vilken SubCategory (parent) som Question (child) tillhör 
        public SubCategoryModel SubCategory { get; set; } // navigation property som låter oss navigera upp till parent (SubCategory) från child (Question), many to one, många Questions kan tillhöra en SubCategoryModel

        //lista av typen AnswerOptionModel, namnet på listan är AnswerOptions, vi hämtar listan med get; och sätter till värdet av listan med set; men
        //om det inte finns något värde i listan, sätts den till tom = []; (samma sak som = new();) för att undvika krasher 
        public List<AnswerOptionModel> AnswerOptions { get; set; } = []; //AnswerOptions är en navigation property, låter oss navigera ner till AnswerOptions, one to many, en QuestionModel kan ha många AnswerOptionModels
        public List<UserAnswerModel> UserAnswers { get; set; } = []; //UserAnswers är en navigation property, låter oss navigera ner till UserAnswers, one to many, en QuestionModel kan ha många UserAnswerModels

    }
}
