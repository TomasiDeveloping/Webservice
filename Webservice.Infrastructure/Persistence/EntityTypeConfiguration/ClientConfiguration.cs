using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webservice.Domain.Entities;
using Webservice.Infrastructure.Constants;

namespace Webservice.Infrastructure.Persistence.EntityTypeConfiguration;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable(nameof(Client), DbSchemas.Api);
        builder.HasKey(client => client.Id);

        builder.Property(client => client.Id)
            .ValueGeneratedNever();

        builder.Property(client => client.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(client => client.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(client => client.ClientTier)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(client => client.CreatedAtUtc)
            .IsRequired();
    }
}