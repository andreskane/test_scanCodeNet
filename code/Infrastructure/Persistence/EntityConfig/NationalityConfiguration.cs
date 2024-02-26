using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig
{
    public class NationalityConfiguration : IEntityTypeConfiguration<Nationality>
    {
        public void Configure(EntityTypeBuilder<Nationality> builder)
        {
            string table_name = "Nationality";
            builder.ToTable(table_name, ApplicationDbContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(x => x.Id).HasColumnName("Code");
            builder.Property(b => b.Name).HasColumnName("Nationality");
        }
    }
}
