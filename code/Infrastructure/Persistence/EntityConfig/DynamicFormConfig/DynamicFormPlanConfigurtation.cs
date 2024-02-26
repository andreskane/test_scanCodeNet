using Domain.Entities.DynamicFormAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig.DynamicFormConfig
{
    public class DynamicFormPlanConfigurtation : IEntityTypeConfiguration<DynamicFormPlan>
    {
        public void Configure(EntityTypeBuilder<DynamicFormPlan> builder)
        {
            string tableName = "DynamicFormPlans";
            builder.ToTable(tableName, ApplicationDbContext.DEFAULT_SCHEMA);
            // Set primary key
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            // Composite key
            builder.HasKey(x => new { x.DynamicFormId, x.PlanId });

            builder.HasOne(x => x.DynamicForm)
                .WithMany(x => x.DynamicFormPlans)
                .HasForeignKey(x => x.DynamicFormId);
        }
    }
}
