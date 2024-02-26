using Domain.Entities.DynamicFormAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig
{
    public class DynamicFormTemplateConfiguration : EntityConfigurationBase<DynamicFormTemplate>, IEntityTypeConfiguration<DynamicFormTemplate>
    {
        public void Configure(EntityTypeBuilder<DynamicFormTemplate> builder)
        {
            string table_name = nameof(DynamicFormTemplate);
            builder.ToTable(table_name, ApplicationDbContext.DEFAULT_SCHEMA);

            builder.HasKey(t => t.Id);
            builder.Property<Int64>(cr => cr.Id).HasColumnName("id");

            builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(255).IsRequired();
            builder.Property(x => x.Description).HasColumnName("Description");
            builder.Property(x => x.State).HasColumnName("State").IsRequired();
            builder.Property(x => x.Version).HasColumnName("Version");

            builder.HasQueryFilter(_ => !_.IsDeleted);

            // Map Auditing Properties
            MapAuditingProperties(builder);
        }
    }
}
