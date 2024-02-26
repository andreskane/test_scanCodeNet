using Domain.Entities.RulesAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig
{
    public class RuleConfiguration : EntityConfigurationBase<RuleDynamic>, IEntityTypeConfiguration<RuleDynamic>
    {
        public void Configure(EntityTypeBuilder<RuleDynamic> builder)
        {
            string table_name = nameof(RuleDynamic);
            builder.ToTable(table_name, ApplicationDbContext.DEFAULT_SCHEMA);

            builder.HasKey(t => t.Id);
            builder.Property<Int64>(cr => cr.Id).HasColumnName("id");

            builder.Property<string>(cr => cr.Name);
            builder.Property<string>(cr => cr.Description);
            builder.Property<bool>(cr => cr.Enabled);
            builder.Property<string>(cr => cr.Version);
            builder.Property<string>(cr => cr.KeyDocument);
            builder.HasQueryFilter(_ => !_.IsDeleted);

            // Map Auditing Properties
            MapAuditingProperties(builder);
        }

    }
}
