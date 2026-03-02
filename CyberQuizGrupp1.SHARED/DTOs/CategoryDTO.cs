namespace CyberQuizGrupp1.SHARED.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public int TotalSubCategories { get; set; }
        public int CompletedSubCategories { get; set; }
        public List<SubCategoryDTO> SubCategories { get; set; } = [];

    }
}
