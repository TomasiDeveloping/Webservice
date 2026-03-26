using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webservice.Domain.Entities;
using Webservice.Infrastructure.Constants;
using Webservice.Infrastructure.Identity;

namespace Webservice.Infrastructure.Persistence.EntityTypeConfiguration;

public class ClientUserConfiguration: IEntityTypeConfiguration<ClientUser>
{
    public void Configure(EntityTypeBuilder<ClientUser> builder)
    {
        builder.ToTable(nameof(ClientUser), DbSchemas.Api);
        builder.HasKey(clientUser => clientUser.Id);

        builder.Property(clientUser => clientUser.Id)
            .ValueGeneratedNever();

        builder.HasOne(clientUser => clientUser.Client)
            .WithMany(client => client.Users)
            .HasForeignKey(clientUser => clientUser.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(clientUser => clientUser.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(clientUser => new
            {
                clientUser.ClientId, 
                clientUser.UserId
            }).IsUnique();
    }
}