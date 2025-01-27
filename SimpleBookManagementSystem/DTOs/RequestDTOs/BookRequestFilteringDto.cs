namespace SimpleBookManagementSystem.DTOs.RequestDTOs
{
    public class BookRequestFilteringDto
    {
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public double Price { get; set; } = 0f;
    }
}
