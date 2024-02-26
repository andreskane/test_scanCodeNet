using Domain.Entities.RulesAggregate;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig
{
    public class RuleActionConfig : EntityConfigurationBase<RuleAction>, IEntityTypeConfiguration<RuleAction>
    {
        public void Configure(EntityTypeBuilder<RuleAction> builder)
        {
            string table_name = nameof(RuleAction);
            builder.ToTable(table_name, ApplicationDbContext.DEFAULT_SCHEMA);

            builder.HasKey(t => t.Id);
            builder.Property<Int64>(cr => cr.Id).HasColumnName("id");

            builder.Property<RuleType>(cr => cr.ActionType);
            builder.Property(cr => cr.RequestType).IsRequired(false);
            builder.Property(cr => cr.RequestBy).IsRequired(false);

            builder.Property<string>(cr => cr.Name);
            builder.Property<string>(cr => cr.Description).IsRequired(false);
            builder.Property<string>(cr => cr.Version);
            builder.Property<bool>(cr => cr.IsActive);
            builder.Property<string>(cr => cr.Url).IsRequired(false);
            builder.Property<string>(cr => cr.WhenScript).HasColumnType("nvarchar(max)").IsRequired(false);
            builder.Property<string>(cr => cr.ThenScript).HasColumnType("nvarchar(max)").IsRequired(false);

            builder.Property<int>(cr => cr.Order);
            builder.Property<Int64>(cr => cr.RuleId);


            builder.HasOne(d => d.Rule)
                .WithMany(p => p.Actions)
                .HasForeignKey(d => d.RuleId);

            builder.HasQueryFilter(_ => !_.IsDeleted);

            // Map Auditing Properties
            MapAuditingProperties(builder);
        }
    }
}
