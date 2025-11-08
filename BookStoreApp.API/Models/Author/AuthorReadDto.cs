using BookStoreApp.API.Models.Book;

namespace BookStoreApp.API.Models.Author
{
    public class AuthorReadDto : BaseDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Bio { get; set; }

        public string FullName => $"{FirstName} {LastName}".Trim();

        public ICollection<AuthorBookReadDto> Books { get; set; } = new List<AuthorBookReadDto>();
    }
}
