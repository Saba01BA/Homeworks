using System.ComponentModel.DataAnnotations;

namespace BookManagementSystem.Models
{
    public class CreateBookRequest
    {
        [Required]
        [MinLength(2, ErrorMessage = "Book Title must be at least 2 characters long.")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(3, ErrorMessage = "Authors Name must be at least 3 characters long.")]
        public string Author { get; set; } = string.Empty;

        [Range(1900, 2026, ErrorMessage = "The value must be between 1900 and 2026.")]
        public int PublishYear { get; set; }

        public string Genre { get; set; } = string.Empty;

        public bool IsAvailable { get; set; }
    }
}
