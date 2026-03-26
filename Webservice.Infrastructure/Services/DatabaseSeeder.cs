using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Webservice.Infrastructure.Identity;
using Webservice.Infrastructure.Persistence;
using Webservice.Infrastructure.Settings;
using Webservice.Shared.Constants;

namespace Webservice.Infrastructure.Services;

public class DatabaseSeeder(ILogger<DatabaseSeeder> logger, AppDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, IOptions<DatabaseSettings> options)
{
    private readonly DatabaseSettings _databaseSettings = options.Value;

    public async Task SeedAsync()
    {
        if (_databaseSettings.AutoMigrate)
        {
            logger.LogInformation("Applying database migrations...");
            await dbContext.Database.MigrateAsync();
        }

        if (!_databaseSettings.Seed)
        {
            logger.LogInformation("Seeding is disabled. Skipping seeding process.");
            return;
        }

        logger.LogInformation("Seeding initial data...");
        var roles = new [] { Roles.SystemAdmin, Roles.CustomerAdmin, Roles.Client };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }
        }

        logger.LogInformation("Ensuring system admin user exists...");
        var adminUser = await userManager.FindByNameAsync(_databaseSettings.SystemAdmin.Email);
        if (adminUser is null)
        {
            adminUser = new ApplicationUser
            {
                UserName = _databaseSettings.SystemAdmin.Email,
                Email = _databaseSettings.SystemAdmin.Email,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(adminUser, _databaseSettings.SystemAdmin.Password);
            await userManager.AddToRoleAsync(adminUser, Roles.SystemAdmin);
        }

        await dbContext.SaveChangesAsync();
    }
}