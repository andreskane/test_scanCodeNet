using Domain.Entities.DynamicFormAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig
{
    public class DynamicFormProductAttributesConfiguration : EntityConfigurationBase<DynamicFormProductAttributes>, IEntityTypeConfiguration<DynamicFormProductAttributes>
    {
        public void Configure(EntityTypeBuilder<DynamicFormProductAttributes> builder)
        {
            string table_name = nameof(DynamicFormProductAttributes);
            builder.ToTable(table_name, ApplicationDbContext.DEFAULT_SCHEMA);

            builder.HasKey(t => t.Id);
            builder.Property<Int64>(cr => cr.Id).HasColumnName("id");

            builder.Property(x => x.ZipCode).HasColumnName("ZipCode").HasMaxLength(255).IsRequired();
            builder.Property(x => x.AtributteCode).HasColumnName("AtributteCode").HasMaxLength(50);
            builder.Property(x => x.DataType).HasColumnName("DataType");
            builder.Property(x => x.DefaultValue).HasColumnName("DefaultValue");
            builder.Property(x => x.ExtraInfo).HasColumnName("ExtraInfo");
            builder.Property(x => x.Metadata).HasColumnName("Metadata");
            builder.Property(x => x.Optional).HasColumnName("Optional");
            builder.Property(x => x.DynamicFormId).HasColumnName("DynamicFormId");
            builder.Property(x => x.ProductId).HasColumnName("ProductId");


            builder.HasOne(d => d.DynamicForm)
                 .WithMany(p => p.Attributes)
                 .HasForeignKey(d => d.DynamicFormId);


            builder.HasQueryFilter(_ => !_.IsDeleted);

            // Map Auditing Properties
            MapAuditingProperties(builder);
        }
    }
}
