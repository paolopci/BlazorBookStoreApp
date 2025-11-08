namespace BookStoreApp.API.Models.Book
{
    public class AuthorBookReadDto : BaseDto
    {
        public string Title { get; set; } = string.Empty;
        public int? Year { get; set; }
        public string Isbn { get; set; } = string.Empty;
        public string? Summary { get; set; }
        public string? Image { get; set; }
        public decimal? Price { get; set; }
        public int? AuthorId { get; set; }
    }
}
