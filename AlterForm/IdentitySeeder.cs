using Microsoft.AspNetCore.Identity;
using AlterForm;

public static class IdentitySeeder
{
    public static async Task SeedAsync(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var sp = scope.ServiceProvider;

        var roleManager = sp.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        foreach (var role in AlterRoles.All)
        {
            if (await roleManager.RoleExistsAsync(role)) continue;
            var result = await roleManager.CreateAsync(new IdentityRole<Guid>(role));
            if (!result.Succeeded) throw new InvalidOperationException($"Seeding role '{role}' failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        // Dev admin account: Development only, credentials from user-secrets
        if (!app.Environment.IsDevelopment()) return;

        var email = app.Configuration["AlterAdmin:Email"];
        var password = app.Configuration["AlterAdmin:Password"];
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password)) return;

        var userManager = sp.GetRequiredService<UserManager<AlterUser>>();
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            user = new AlterUser { UserName = email, Email = email, EmailConfirmed = true };
            var created = await userManager.CreateAsync(user, password);
            if (!created.Succeeded) throw new InvalidOperationException($"Seeding dev admin failed: {created.Errors.ToString()}");
        }

        if (!await userManager.IsInRoleAsync(user, AlterRoles.Administrator)) await userManager.AddToRoleAsync(user, AlterRoles.Administrator);
    }
}