using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.SHARED.DTOs
{
    //DTO som UI använder för att visa info för användaren
    public class CoachingResponseDTO
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; } = "";
        public string StrengthSummary { get; set; } = "";
        public string WeaknessSummary { get; set; } = "";
        public string CoachText { get; set; } = "";

    }
}
