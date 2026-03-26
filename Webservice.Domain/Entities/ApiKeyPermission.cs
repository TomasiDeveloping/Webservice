namespace Webservice.Domain.Entities;

public class ApiKeyPermission
{
    public Guid Id { get; set; }

    public Guid ApiKeyId { get; set; }

    public required string Scope { get; set; }

    public required string Description { get; set; }
}