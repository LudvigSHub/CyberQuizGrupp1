using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.SHARED.DTOs
{
    public class CoachingItemDTO
    {
        //DTO för ett objekt (en subkategori som användaren ska få feedback på från AI) i Coaching-listan
        public int SubCategoryId { get; set;  }
        public string SubCategoryName { get; set; } = "";
    }
}
