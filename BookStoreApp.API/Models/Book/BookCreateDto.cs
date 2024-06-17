using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Models.Book
{
    public class BookCreateDto
    {
        [Required]
        public int AuthorId { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        
        [Required]
        [Range(200, int.MaxValue)]
        public int Year { get; set; }
        
        [Required]
        public string Isbn { get; set; } 
        
        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Summary { get; set; }

        public string? ImageData { get; set; }
        public string? ImageName { get; set; }
        
        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }
    }
}
