using Domain.Entities.DynamicFormAggregate;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.EntityConfig
{
    public class TemplateComponentConfig : IEntityTypeConfiguration<TemplateComponent>
    {

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TemplateComponent> builder)
        {
            string table_name = nameof(TemplateComponent);
            builder.ToTable(table_name, ApplicationDbContext.DEFAULT_SCHEMA);

            builder.HasKey(t => t.Id);
            builder.Property<Int64>(cr => cr.Id).HasColumnName("id");

            builder.Property<string>(cr => cr.Name);
            builder.Property<string>(cr => cr.Description).IsRequired(false);
            builder.Property<componentTypeEnum>(cr => cr.DataType);
            builder.Property<string>(cr => cr.Label).IsRequired(false);
            builder.Property<string>(cr => cr.value).IsRequired(false);
            builder.Property<string>(cr => cr.data).IsRequired(false);
            builder.Property<string>(cr => cr.placeholder).IsRequired(false);
            builder.Property<string>(cr => cr.groupID).IsRequired(false);
            builder.Property<Boolean>(cr => cr.IsRequired).HasDefaultValue(false);
            builder.Property<Boolean>(cr => cr.IsHidden).HasDefaultValue(false);

            builder.AddBaseConfiguration();


        }
    }
}
