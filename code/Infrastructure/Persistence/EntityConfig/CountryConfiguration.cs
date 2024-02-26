using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        string table_name = "Country";
        builder.ToTable(table_name, ApplicationDbContext.DEFAULT_SCHEMA);
        builder.HasKey(cr => cr.Id);
        builder.Property(x => x.Id).HasColumnName("Code");
        builder.Property(b => b.Name).HasColumnName("Country");
    }
}
