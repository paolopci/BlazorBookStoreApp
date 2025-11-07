using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Data
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Authors");
                entity.HasKey(a => a.Id);

                entity.Property(a => a.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(a => a.FirstName)
                      .HasMaxLength(50);

                entity.Property(a => a.LastName)
                      .HasMaxLength(50);

                entity.Property(a => a.Bio)
                      .HasMaxLength(250);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Books");
                entity.HasKey(b => b.Id);

                entity.Property(b => b.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(b => b.Title)
                      .HasMaxLength(50);

                entity.Property(b => b.Year);

                entity.Property(b => b.Isbn)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasIndex(b => b.Isbn)
                      .IsUnique();

                entity.Property(b => b.Summary)
                      .HasMaxLength(250);

                entity.Property(b => b.Image)
                      .HasMaxLength(50);

                entity.Property(b => b.Price)
                      .HasColumnType("decimal(18, 2)");

                entity.Property(b => b.AuthorId);

                entity.HasOne(b => b.Author)
                      .WithMany(a => a.Books)
                      .HasForeignKey(b => b.AuthorId)
                      .HasConstraintName("FK_Books_ToTable");
            });
        }
    }
}
