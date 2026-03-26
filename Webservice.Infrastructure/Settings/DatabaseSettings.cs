namespace Webservice.Infrastructure.Settings;

public class DatabaseSettings
{
    public bool AutoMigrate { get; set; }
    public bool Seed { get; set; }
    public bool SeedOnlyIfEmpty { get; set; }
    public SystemAdminSettings SystemAdmin { get; set; } = new();
}

public class SystemAdminSettings
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}