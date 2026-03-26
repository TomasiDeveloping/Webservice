using Webservice.Domain.Enums;

namespace Webservice.Domain.Entities;

public class ClientUser
{
    public Guid Id { get; set; }

    public Guid ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public Guid UserId { get; set; }

    public ClientRole Role { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}