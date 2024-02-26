using Domain.Entities.RulesAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig
{
    public class DynamicFormRuleConfiguration : EntityConfigurationBase<DynamicFormRule>, IEntityTypeConfiguration<DynamicFormRule>
    {
        public void Configure(EntityTypeBuilder<DynamicFormRule> builder)
        {
            string table_name = nameof(DynamicFormRule);
            builder.ToTable(table_name, ApplicationDbContext.DEFAULT_SCHEMA);

            builder.HasKey(t => t.Id);
            builder.Property<Int64>(cr => cr.Id).HasColumnName("id");

            builder.HasOne(d => d.DynamicForm)
                 .WithMany(p => p.DynamicFormRules)
                 .HasForeignKey(d => d.DynamicFormId).OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.Rule)
                 .WithMany(p => p.DynamicFormRules)
                 .HasForeignKey(d => d.RuleId).OnDelete(DeleteBehavior.NoAction);


            builder.HasQueryFilter(_ => !_.IsDeleted);

            // Map Auditing Properties
            MapAuditingProperties(builder);
        }
    }
}
