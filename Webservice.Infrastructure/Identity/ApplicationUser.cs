using Microsoft.AspNetCore.Identity;
using Webservice.Domain.Entities;

namespace Webservice.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public ICollection<ClientUser> Clients { get; set; } = [];
}