using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Webservice.Domain.Entities;
using Webservice.Infrastructure.Constants;

namespace Webservice.Infrastructure.Persistence.EntityTypeConfiguration;

public class ApiKeyPermissionConfiguration : IEntityTypeConfiguration<ApiKeyPermission>
{
    public void Configure(EntityTypeBuilder<ApiKeyPermission> builder)
    {
        builder.ToTable(nameof(ApiKeyPermission), DbSchemas.Api);
        builder.HasKey(apiKeyPermission => apiKeyPermission.Id);

        builder.Property(apiKeyPermission => apiKeyPermission.Id)
            .ValueGeneratedNever();

        builder.Property(apiKeyPermission => apiKeyPermission.Scope)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(apiKeyPermission => apiKeyPermission.Description)
            .IsRequired()
            .HasMaxLength(250);

        builder.HasOne<ApiKey>()
            .WithMany(apiKey => apiKey.Permissions)
            .HasForeignKey(apiKeyPermission => apiKeyPermission.ApiKeyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(apiKeyPermission => new { apiKeyPermission.ApiKeyId, apiKeyPermission.Scope })
            .IsUnique();
    }
}