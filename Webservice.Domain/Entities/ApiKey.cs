namespace Webservice.Domain.Entities;

public class ApiKey
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public required string Key { get; set; }
    public required string Prefix { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? ExpiredAtUtc { get; set; }
    public bool IsActive { get; set; }

    public ICollection<ApiKeyPermission> Permissions { get; set; } = [];
}