using Domain.Entities.DynamicFormAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig
{
    public class DynamicFormItemConfiguration : EntityConfigurationBase<DynamicFormItem>, IEntityTypeConfiguration<DynamicFormItem>
    {
        public void Configure(EntityTypeBuilder<DynamicFormItem> builder)
        {
            string table_name = nameof(DynamicFormItem);
            builder.ToTable(table_name, ApplicationDbContext.DEFAULT_SCHEMA);

            builder.HasKey(t => t.Id);
            builder.Property<Int64>(cr => cr.Id).HasColumnName("id");

            builder.Property(x => x.Code).HasColumnName("Code").IsRequired(false);
            builder.Property(x => x.Layout).HasColumnName("Layout").HasColumnType("nvarchar(max)");
            builder.Property(x => x.Version).HasColumnName("Version");
            builder.Property(x => x.Status).HasColumnName("Status");
            builder.Property(x => x.CodeFlow).HasColumnName("CodeFlow");
            builder.Property(x => x.Description).HasColumnName("Description");

            //builder.HasOne(s => s.Workflow)
            //    .WithOne()
            //    .HasForeignKey<DynamicFormItem>(s => s.DynamicFormId);

            //builder.HasOne(s => s.WorkflowTemplate)
            //    .WithOne()
            //    .HasForeignKey<DynamicFormItem>(s => s.DynamicFormTemplateId);

            builder.HasOne(c => c.DynamicForm)
    .WithMany(a => a.FlowList)
    .HasForeignKey(c => c.DynamicFormId);

            builder.HasOne(c => c.DynamicFormTemplate)
                .WithMany(b => b.FlowList)
                .HasForeignKey(c => c.DynamicFormTemplateId);

            builder.HasQueryFilter(_ => !_.IsDeleted);

            // Map Auditing Properties
            MapAuditingProperties(builder);
        }
    }
}
