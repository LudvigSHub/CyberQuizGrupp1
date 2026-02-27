using CyberQuizGrupp1.SHARED.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CyberQuizGrupp1.SHARED.DTOs
{
    public class SubCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsLocked { get; set; } = true;
        public bool IsCompleted { get; set; } = false;
        public int QuestionCount { get; set; }
    }
}
