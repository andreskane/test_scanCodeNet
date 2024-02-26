using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig
{
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            string table_name = "Gender";
            builder.ToTable(table_name, ApplicationDbContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(x => x.Id).HasColumnName("Code");
            builder.Property(b => b.Name).HasColumnName("Gender");
        }
    }
}
