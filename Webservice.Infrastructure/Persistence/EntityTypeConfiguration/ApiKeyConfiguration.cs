using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webservice.Domain.Entities;
using Webservice.Infrastructure.Constants;

namespace Webservice.Infrastructure.Persistence.EntityTypeConfiguration;

public class ApiKeyConfiguration : IEntityTypeConfiguration<ApiKey>
{
    public void Configure(EntityTypeBuilder<ApiKey> builder)
    {
        builder.ToTable(nameof(ApiKey), DbSchemas.Api);
        builder.HasKey(apiKey => apiKey.Id);

        builder.Property(apiKey => apiKey.Id)
            .ValueGeneratedNever();

        builder.Property(apiKey => apiKey.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(apiKey => apiKey.Prefix)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(apiKey => apiKey.Key)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(apiKey => apiKey.IsActive)
            .IsRequired();

        builder.Property(apiKey => apiKey.CreatedAtUtc)
            .IsRequired();

        builder.Property(apiKey => apiKey.ExpiredAtUtc)
            .IsRequired(false);

        builder.HasOne(apiKey => apiKey.Client)
            .WithMany(client => client.ApiKeys)
            .HasForeignKey(apiKey => apiKey.ClientId);

        builder.HasIndex(apiKey => apiKey.Key).IsUnique();
    }
}