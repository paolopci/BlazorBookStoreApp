using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Models.Book
{
    public class BookCreateDto
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int? Year { get; set; }

        [Required]
        [StringLength(50)]
        public string Isbn { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Summary { get; set; }

        [StringLength(50)]
        public string? Image { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Price { get; set; }

        [Required]
        public int AuthorId { get; set; }
    }
}
