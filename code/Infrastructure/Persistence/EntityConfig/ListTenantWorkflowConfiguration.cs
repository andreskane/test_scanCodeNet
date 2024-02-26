using Domain.Entities.ListAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig
{
    public class ListTenantWorkflowConfiguration : EntityConfigurationBase<ListTenantWorkflow>, IEntityTypeConfiguration<ListTenantWorkflow>
    {
        public void Configure(EntityTypeBuilder<ListTenantWorkflow> builder)
        {
            string table_name = nameof(ListTenantWorkflow);
            builder.ToTable(table_name, ApplicationDbContext.DEFAULT_SCHEMA);

            builder.HasKey(t => t.Id);
            builder.Property<Int64>(cr => cr.Id).HasColumnName("id");

            builder.Property<string>(cr => cr.TenantId);

            builder.HasOne(d => d.ListDefinition)
                .WithMany(p => p.ListsTenantsWorkflows)
                .HasForeignKey(d => d.ListId);

            builder.HasOne(d => d.Workflow)
                .WithMany(p => p.ListTenantWorkflows)
                .HasForeignKey(d => d.WorkflowId);

            builder.HasQueryFilter(_ => !_.IsDeleted);

            // Map Auditing Properties
            MapAuditingProperties(builder);
        }
    }
}
