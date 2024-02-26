using ConnectureOS.Framework.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig;

internal static class AuditableConfiguration
{
    internal static void AddAuditConfiguration<T>(this EntityTypeBuilder<T> builder, string tableName)
        where T : AuditableEntity
    {
        builder.Property(x => x.CreatedBy).HasColumnName($"{tableName}_CreatedBy");
        builder.Property(x => x.CreatedByName).HasColumnName($"{tableName}_CreatedByName");
        builder.Property(x => x.CreatedDate).HasColumnName($"{tableName}_CreatedDate");
        builder.Property(x => x.LastModifiedBy).HasColumnName($"{tableName}_LastModifiedBy");
        builder.Property(x => x.LastModifiedByName).HasColumnName($"{tableName}_LastModifiedByName");
        builder.Property(x => x.LastModifiedDate).HasColumnName($"{tableName}_LastModifiedDate");
        builder.Property(x => x.CompanyId).HasColumnName($"{tableName}_TenantId");
    }
}
