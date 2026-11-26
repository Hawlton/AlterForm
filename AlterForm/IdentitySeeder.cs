using Microsoft.AspNetCore.Identity;
using AlterForm;

public static class IdentitySeeder
{
    public static async Task SeedAsync(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var sp = scope.ServiceProvider;

        var role_manager = sp.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        foreach (var role in AlterRoles.All)
        {
            if (await role_manager.RoleExistsAsync(role)) continue;
            var result = await role_manager.CreateAsync(new IdentityRole<Guid>(role));
            if (!result.Succeeded) throw new InvalidOperationException($"Seeding role '{role}' failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        // Dev admin account: Development only, credentials from user-secrets
        if (!app.Environment.IsDevelopment()) return;

        var email = app.Configuration["AlterAdmin:Email"];
        var password = app.Configuration["AlterAdmin:Password"];
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password)) return;

        var user_manager = sp.GetRequiredService<UserManager<AlterUser>>();
        var user = await user_manager.FindByEmailAsync(email);
        if (user is null)
        {
            user = new AlterUser { UserName = email, Email = email, EmailConfirmed = true };
            var created = await user_manager.CreateAsync(user, password);
            if (!created.Succeeded) throw new InvalidOperationException($"Seeding dev admin failed: {created.Errors.ToString()}");
            var created_user = await user_manager.FindByEmailAsync(email);
            if(created_user == null) throw new InvalidOperationException("This shouldn't be possible");
            await user_manager.AddToRoleAsync(created_user, AlterRoles.Administrator);

        }
        else Console.WriteLine($"Found Admin Account: {user.Email}");

        if (!await user_manager.IsInRoleAsync(user, AlterRoles.Administrator)) await user_manager.AddToRoleAsync(user, AlterRoles.Administrator);
    }
}