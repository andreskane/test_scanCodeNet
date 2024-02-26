using Domain.Entities.RulesAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig
{
    public class ActionParameterConfig : EntityConfigurationBase<ActionParameter>, IEntityTypeConfiguration<ActionParameter>
    {
        public void Configure(EntityTypeBuilder<ActionParameter> builder)
        {
            string table_name = nameof(ActionParameter);
            builder.ToTable(table_name, ApplicationDbContext.DEFAULT_SCHEMA);

            builder.HasKey(t => t.Id);
            builder.Property<Int64>(cr => cr.Id).HasColumnName("id");

            builder.Property<string>(cr => cr.Name);
            builder.Property<string>(cr => cr.DataType);
            builder.Property<bool>(cr => cr.IsRequest);
            builder.Property<Int64>(cr => cr.RuleActtioId);

            builder.HasOne(d => d.RuleAction)
                .WithMany(p => p.Parameters)
                .HasForeignKey(d => d.RuleActtioId);

            builder.HasQueryFilter(_ => !_.IsDeleted);

            // Map Auditing Properties
            MapAuditingProperties(builder);
        }
    }
}
