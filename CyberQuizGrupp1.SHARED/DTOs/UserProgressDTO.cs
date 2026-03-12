using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.SHARED.DTOs
{
    public class UserProgressDTO
    {
        public int TotalSubCategories { get; set; }
        public int CompletedSubCategories { get; set; }
        public double ProgressPercentage { get; set; }
    }
}
