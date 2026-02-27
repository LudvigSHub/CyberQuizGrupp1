using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.SHARED.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //lista av typen SubCategoryModel, namnet på listan är SubCategories, vi hämtar listan med get; och sätter till värdet av listan med set; men
        //om det inte finns något värde i listan, sätts den till tom = []; (samma sak som = new();) för att undvika krasher 
        public List<SubCategoryModel> SubCategories { get; set; } = []; //SubCategories är en navigation property, one to many, en CategoryModel kan ha många SubCategoryModels


    }
}
