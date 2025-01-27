using System.ComponentModel.DataAnnotations;

namespace SimpleBookManagementSystem.DTOs.RequestDTOs
{
    public class BookRequestDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public double Price { get; set; }
        public int[] CategoryIds { get; set; }
    }
}
