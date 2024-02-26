using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        string table_name = "City";
        builder.ToTable(table_name, ApplicationDbContext.DEFAULT_SCHEMA);
        builder.HasKey(cr => cr.Id);
        builder.Property(x => x.Id).HasColumnName("Code");
        builder.Property(b => b.Name).HasColumnName("Name");
    }
}
