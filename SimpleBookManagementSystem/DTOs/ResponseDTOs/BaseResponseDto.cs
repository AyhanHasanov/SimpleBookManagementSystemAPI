namespace SimpleBookManagementSystem.DTOs.ResponseDTOs
{
    public class BaseResponseDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
