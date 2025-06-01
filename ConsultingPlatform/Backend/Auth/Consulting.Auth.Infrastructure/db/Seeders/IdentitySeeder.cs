using Consulting.Auth.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Consulting.Auth.Infrastructure.db.Seeders;

public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await SeedRolesAsync(roleManager);
        await SeedUsersAsync(userManager);
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = { "Admin", "User" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    private static async Task SeedUsersAsync(UserManager<User> userManager)
    {
        if (!await userManager.Users.AnyAsync())
        {
            var user1 = new User
            {
                UserName = "user1@example.com",
                Email = "user1@example.com",
                EmailConfirmed = true
            };

            var user2 = new User
            {
                UserName = "user2@example.com",
                Email = "user2@example.com",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(user1, "Qwerty123$");
            await userManager.AddToRoleAsync(user1, "User");

            await userManager.CreateAsync(user2, "Qwerty123$");
            await userManager.AddToRoleAsync(user2, "User");
        }
    }
}
