using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Seeders
{
    public class UserRoleSeeder : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData
            (
                new IdentityUserRole<string>
                {
                    RoleId = "1",
                    UserId = "1",
                },
                new IdentityUserRole<string>
                {
                    RoleId = "2",
                    UserId = "2",
                }
            );
        }
    }
}
