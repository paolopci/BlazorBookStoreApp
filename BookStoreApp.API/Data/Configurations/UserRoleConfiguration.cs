using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStoreApp.API.Data.Configurations
{
    internal class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    UserId = IdentitySeedData.AdminUserId,
                    RoleId = IdentitySeedData.AdministratorRoleId
                },
                new IdentityUserRole<string>
                {
                    UserId = IdentitySeedData.RegularUserId,
                    RoleId = IdentitySeedData.UserRoleId
                });
        }
    }
}
