using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig.Bulk;

public class BulkProcessConfig : EntityConfigurationBase<BulkProcess>, IEntityTypeConfiguration<BulkProcess>
{

    public void Configure(EntityTypeBuilder<BulkProcess> builder)
    {
        string table_name = nameof(BulkProcess);
        builder.ToTable(table_name, ApplicationDbContext.DEFAULT_SCHEMA);

        builder.HasKey(t => t.Id);
        builder.Property<Int64>(cr => cr.Id).HasColumnName("id");
        builder.Property(x => x.Name).HasColumnName("Name");
        builder.Property(x => x.ProcessType).HasColumnName("ProcessType");
        builder.Property(x => x.Status).HasColumnName("Status");
        builder.Property(x => x.ErrorMessage).HasColumnName("ErrorMessage");
        builder.Property(x => x.PlacementPreference).HasColumnName("PlacementPreference");
        builder.Property(x => x.ComponentOfReference).HasColumnName("ComponentOfReference");
        builder.Property(x => x.StartDate).HasColumnName("StartDate");
        builder.Property(x => x.EndDate).HasColumnName("EndDate").IsRequired(false);




        // Map Auditing Properties
        MapAuditingProperties(builder);
    }
}



public class ComponentConfig : EntityConfigurationBase<BulckComponent>, IEntityTypeConfiguration<BulckComponent>
{


    public void Configure(EntityTypeBuilder<BulckComponent> builder)
    {
        string table_name = nameof(BulckComponent);
        builder.ToTable(table_name, ApplicationDbContext.DEFAULT_SCHEMA);

        builder.HasKey(t => t.Id);
        builder.Property<Int64>(cr => cr.Id).HasColumnName("id");
        builder.Property(x => x.Name).HasColumnName("Name");
        builder.Property(x => x.Description).HasColumnName("Description").IsRequired(false);
        builder.Property(x => x.typeComponent).HasColumnName("typeComponent");
        builder.Property(x => x.Order).HasColumnName("Order");
        builder.Property(x => x.InputId).HasColumnName("InputId").IsRequired(false);
        builder.Property(x => x.Label).HasColumnName("Label").IsRequired(false);
        builder.Property(x => x.Value).HasColumnName("Value").IsRequired(false);
        builder.Property(x => x.IsHidden).HasColumnName("IsHidden");
        builder.Property(x => x.Required).HasColumnName("Required");

        // Map Auditing Properties
        MapAuditingProperties(builder);
    }
}


public class DynamicFormBulckProcessConfig : EntityConfigurationBase<DynamicFormBulckProcess>, IEntityTypeConfiguration<DynamicFormBulckProcess>
{
    public void Configure(EntityTypeBuilder<DynamicFormBulckProcess> builder)
    {
        string table_name = nameof(DynamicFormBulckProcess);
        builder.ToTable(table_name, ApplicationDbContext.DEFAULT_SCHEMA);

        builder.HasKey(t => t.Id);
        builder.Property<Int64>(cr => cr.Id).HasColumnName("id");

        builder.HasOne(d => d.DynamicForm)
             .WithMany(p => p.DynamicFormBulkProcess)
             .HasForeignKey(d => d.DynamicFormId).OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(d => d.BulkProcess)
             .WithMany(p => p.DynamicFormListItems)
             .HasForeignKey(d => d.BulkProcessId).OnDelete(DeleteBehavior.NoAction);


        builder.HasQueryFilter(_ => !_.IsDeleted);

        // Map Auditing Properties
        MapAuditingProperties(builder);
    }
}