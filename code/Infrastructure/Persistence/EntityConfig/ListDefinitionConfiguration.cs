using Domain.Entities.ListAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig
{
    public class ListDefinitionConfiguration : EntityConfigurationBase<ListDefinition>, IEntityTypeConfiguration<ListDefinition>
    {
        public void Configure(EntityTypeBuilder<ListDefinition> builder)
        {
            string table_name = nameof(ListDefinition);
            builder.ToTable(table_name, ApplicationDbContext.DEFAULT_SCHEMA);

            builder.HasKey(t => t.Id);
            builder.Property<Int64>(cr => cr.Id).HasColumnName("id");

            builder.Property(x => x.ListName).HasColumnName("ListName").HasMaxLength(255).IsRequired();
            builder.Property(x => x.Description).HasColumnName("Description").HasMaxLength(255).IsRequired(false);
            builder.Property(x => x.KeyName).HasColumnName("KeyName").IsRequired();
            builder.Property(x => x.ValueName).HasColumnName("ValueName");
            builder.Property(x => x.DataType).HasColumnName("DataType").IsRequired();

            builder.HasQueryFilter(_ => !_.IsDeleted);

            // Map Auditing Properties
            MapAuditingProperties(builder);
        }
    }
}
