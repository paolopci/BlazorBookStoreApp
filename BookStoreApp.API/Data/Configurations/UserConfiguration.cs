using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStoreApp.API.Data.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<ApiUser>
    {
        public void Configure(EntityTypeBuilder<ApiUser> builder)
        {
            builder.HasData(
                new ApiUser
                {
                    Id = IdentitySeedData.AdminUserId,
                    Email = "testr.admin@bookstore.com",
                    NormalizedEmail = "TESTR.ADMIN@BOOKSTORE.COM",
                    UserName = "testr.admin@bookstore.com",
                    NormalizedUserName = "TESTR.ADMIN@BOOKSTORE.COM",
                    FirstName = "Testr",
                    LastName = "Admin",
                    EmailConfirmed = true,
                    PasswordHash = IdentitySeedData.AdminPasswordHash,
                    ConcurrencyStamp = IdentitySeedData.AdminConcurrencyStamp,
                    SecurityStamp = IdentitySeedData.AdminSecurityStamp
                },
                new ApiUser
                {
                    Id = IdentitySeedData.RegularUserId,
                    Email = "testr.user@bookstore.com",
                    NormalizedEmail = "TESTR.USER@BOOKSTORE.COM",
                    UserName = "testr.user@bookstore.com",
                    NormalizedUserName = "TESTR.USER@BOOKSTORE.COM",
                    FirstName = "Testr",
                    LastName = "User",
                    EmailConfirmed = true,
                    PasswordHash = IdentitySeedData.UserPasswordHash,
                    ConcurrencyStamp = IdentitySeedData.UserConcurrencyStamp,
                    SecurityStamp = IdentitySeedData.UserSecurityStamp
                });
        }
    }
}
