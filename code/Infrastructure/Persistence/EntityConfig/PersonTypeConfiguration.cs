using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig
{
    public class PersonTypeConfiguration : IEntityTypeConfiguration<PersonType>
    {
        public void Configure(EntityTypeBuilder<PersonType> builder)
        {
            string table_name = "PersonType";
            builder.ToTable(table_name, ApplicationDbContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(x => x.Id).HasColumnName("Code");
            builder.Property(b => b.Name).HasColumnName("PersonType");
        }
    }
}
