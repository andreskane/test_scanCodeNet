using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig
{
    public class StatusConfiguration : IEntityTypeConfiguration<PersonStatus>
    {
        public void Configure(EntityTypeBuilder<PersonStatus> builder)
        {
            string table_name = "PersonStatus";
            builder.ToTable(table_name, ApplicationDbContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(x => x.Id).HasColumnName("Code");
            builder.Property(b => b.Name).HasColumnName("PersonStatus");
        }
    }
}
