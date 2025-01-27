using System.ComponentModel.DataAnnotations;

namespace SimpleBookManagementSystem.DTOs.RequestDTOs
{
    public class CategoryRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Book title must be at least 3 characters long")]
        [MaxLength(500, ErrorMessage = "Book title must be at max 500 characters long")]
        public string Title { get; set; }
    }
}
