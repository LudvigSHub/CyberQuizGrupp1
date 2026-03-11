using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.SHARED.DTOs
{
    public class CoachingResponseDTO
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; } = string.Empty;
        
        public string StrengthSummary { get; set; } = string.Empty;
        public string WeaknessSummary { get; set; } = string.Empty;
        public string CoachText { get; set; } = string.Empty;


    }
}
