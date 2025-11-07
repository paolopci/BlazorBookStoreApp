using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Data
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
            : base(options)
        {
        }
    }
}
