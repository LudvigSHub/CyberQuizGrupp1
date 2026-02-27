using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.SHARED.Models
{
    public class SubCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string Description { get; set; }
        public bool IsLocked { get; set; } = true; //kategorin är default låst tills användaren låser upp efter att ha klarat 80% av frågorna
        public int CategoryId { get; set; } // foreign key som kopplar ihop och berättar vilken Categori (parent) som subCatergory (child) tillhör  
        public CategoryModel Category { get; set; } // navigation property som låter oss navigera upp till parent (Category) från child (SubCategory), many to one, många SubCategoryModels kan tillhöra en CategoryModel

        //lista av typen QuestionModel, namnet på listan är Questions, vi hämtar listan med get; och sätter till värdet av listan med set; men
        //om det inte finns något värde i listan, sätts den till tom = []; (samma sak som = new();) för att undvika krasher 
        public List<QuestionModel> Questions { get; set; } = []; //Questions är en navigation property, låter oss navigera ner till Questions, one to many, en SubCategoryModel kan ha många QuestionModels
    }
}
