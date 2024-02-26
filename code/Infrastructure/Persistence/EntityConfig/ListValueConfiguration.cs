using Domain.Entities.ListAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig
{
    public class ListValueConfiguration : EntityConfigurationBase<ListValue>, IEntityTypeConfiguration<ListValue>
    {
        public void Configure(EntityTypeBuilder<ListValue> builder)
        {
            string table_name = nameof(ListValue);
            builder.ToTable(table_name, ApplicationDbContext.DEFAULT_SCHEMA);

            builder.HasKey(t => t.Id);
            builder.Property<Int64>(cr => cr.Id).HasColumnName("id");

            builder.Property<string>(cr => cr.Key);
            builder.Property<string>(cr => cr.Value).IsRequired(false);

            builder.HasOne(d => d.ListDefinition)
                .WithMany(p => p.ListValues)
                .HasForeignKey(d => d.ListId);

            builder.HasQueryFilter(_ => !_.IsDeleted);

            // Map Auditing Properties
            MapAuditingProperties(builder);
        }
    }
}