using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.SHARED.Models
{
    public class UserResultModel
    {
        public int Id { get; set; }
        public string UserId { get; set; } // kopplar UserResult till Identity användaren
        public int SubCategoryId { get; set; } // foreign key som kopplar ihop och berättar vilken SubCategori (parent) som UserResult (child) tillhör
        public SubCategoryModel SubCategory { get; set; } // navigation property som låter oss navigera upp till parent (SubCategory) från child (UserResult), many to one, många UserResults kan tillhöra en SubCategoryModel
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        
    }
}
