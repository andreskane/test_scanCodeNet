using Domain.Entities.DynamicFormAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig.DynamicFormConfig
{
    public class DynamicFormComponentRuleConfiguration : IEntityTypeConfiguration<DynamicFormComponentRule>
    {
        public void Configure(EntityTypeBuilder<DynamicFormComponentRule> builder)
        {
            string tableName = "DynamicFormComponentRule";
            builder.ToTable(tableName, ApplicationDbContext.DEFAULT_SCHEMA);

            // Set primary key with auto-incremented Id
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.DynamicFormItemId).HasColumnName("DynamicFormItemId").IsRequired(true);
            builder.Property(x => x.ComponentName).HasColumnName("ComponentName").IsRequired(true);
            builder.Property(x => x.ComponentPropertyId).HasColumnName("ComponentPropertyId").IsRequired(true);
            builder.Property(x => x.RuleId).HasColumnName("RuleId").IsRequired(false);
            builder.Property(x => x.DataType).HasColumnName("DataType").IsRequired(false);
        }
    }
}
