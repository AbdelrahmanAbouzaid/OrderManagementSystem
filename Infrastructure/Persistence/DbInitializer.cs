

using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Services.Abstractions;
using System.Security.Cryptography;
using System.Text;

namespace Persistence
{
    public class DbInitializer(OrderManagementDbContext context) : IDbInitializer
    {
        public async Task InitializeUserAsync()
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                await context.Database.MigrateAsync();
            }

            if (!context.Users.Any())
            {
                var user = new User
                {
                    Username = "Admin",
                    Role = UserRole.Admin,
                    PasswordHash = HashPassword("password")
                };

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }
        }


        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }
    }
}
