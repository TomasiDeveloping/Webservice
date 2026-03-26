using Webservice.Domain.Enums;

namespace Webservice.Domain.Entities;

public class Client
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public ClientTier  ClientTier { get; set; }
    public DateTime CreatedAtUtc { get; set; }


    public ICollection<ClientUser> Users { get; set; } = [];
    public ICollection<ApiKey> ApiKeys { get; set; } = [];
}