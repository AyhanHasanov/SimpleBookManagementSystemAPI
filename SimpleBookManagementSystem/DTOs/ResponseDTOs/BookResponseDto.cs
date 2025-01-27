namespace SimpleBookManagementSystem.DTOs.ResponseDTOs
{
    public class BookResponseDto : BaseResponseDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public List<CategoryResponseDto> Categories { get; set; }
    }
}
