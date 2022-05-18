using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Seeders
{
    public class UserSeeder : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            var passwordHash = hasher.HashPassword(null, "12345678");
            builder.HasData
            (
                new ApplicationUser
                {
                    Id = "1",
                    Email = "admin@localhost.com",
                    NormalizedEmail = "admin@localhost.com",
                    FirstName = "FirstName 1",
                    LastName = "LastName 1",
                    UserName = "admin",
                    NormalizedUserName = "admin",
                    PasswordHash = passwordHash,
                    EmailConfirmed = true,
                },
                new ApplicationUser
                {
                    Id = "2",
                    Email = "customRole@localhost.com",
                    NormalizedEmail = "customRole@localhost.com",
                    FirstName = "FirstName 2",
                    LastName = "LastName 2",
                    UserName = "customRole",
                    NormalizedUserName = "customRole",
                    PasswordHash = passwordHash,
                    EmailConfirmed = true,
                }
            );
        }
    }
}
