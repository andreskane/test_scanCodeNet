using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig
{
    public class EntityConfigurationBase<TEntity> where TEntity : BaseAuditableEntity
    {
        public void MapAuditingProperties(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(e => e.Created).HasColumnName("Created");
            builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy");
            builder.Property(e => e.LastModified).HasColumnName("LastModified");
            builder.Property(e => e.LastModifiedBy).HasColumnName("LastModifiedBy");
            builder.Property(e => e.Deleted).HasColumnName("Deleted");
            builder.Property(e => e.DeletedBy).HasColumnName("DeletedBy");
            builder.Property(e => e.IsDeleted).HasColumnName("IsDeleted");
        }
    }
}
