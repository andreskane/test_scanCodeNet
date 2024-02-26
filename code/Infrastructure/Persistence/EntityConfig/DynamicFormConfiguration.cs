using Domain.Entities.DynamicFormAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig
{
    public class DynamicFormConfiguration : EntityConfigurationBase<DynamicForm>, IEntityTypeConfiguration<DynamicForm>
    {
        public void Configure(EntityTypeBuilder<DynamicForm> builder)
        {
            string table_name = nameof(DynamicForm);
            builder.ToTable(table_name, ApplicationDbContext.DEFAULT_SCHEMA);

            builder.HasKey(t => t.Id);
            builder.Property<Int64>(cr => cr.Id).HasColumnName("id");

            builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(255).IsRequired();
            builder.Property(x => x.Description).HasColumnName("Description");
            builder.Property(x => x.State).HasColumnName("State").IsRequired();
            builder.Property(x => x.Version).HasColumnName("Version");
            builder.Property(x => x.MaxVersion).HasColumnName("MaxVersion");
            builder.Property(x => x.CodeFlowActive).HasColumnName("CodeFlowActive").IsRequired(false);
            builder.HasQueryFilter(_ => !_.IsDeleted);

            // Map Auditing Properties
            MapAuditingProperties(builder);
        }
    }
}
