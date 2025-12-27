using EducationSystemBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationSystemBackend.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (await context.Teachers.AnyAsync(t => t.Email == "eylul@ozatman.com")) return;

            // Create the hidden 'OrangeLesson' organization for the admin
            var adminOrg = await context.Organizations.FirstOrDefaultAsync(o => o.Name == "OrangeLesson");
            if (adminOrg == null)
            {
                adminOrg = new Organization { Name = "OrangeLesson", IsHidden = true };
                context.Organizations.Add(adminOrg);
                await context.SaveChangesAsync();
            }

            var admin = new Teacher
            {
                FullName = "Eylül Özatman",
                Email = "eylul@ozatman.com",
                Password = "admin", // In production use hashed password
                City = "Istanbul",
                OrganizationId = adminOrg.Id,
                Role = UserRole.Admin
            };

            context.Teachers.Add(admin);
            await context.SaveChangesAsync();
        }
    }
}
