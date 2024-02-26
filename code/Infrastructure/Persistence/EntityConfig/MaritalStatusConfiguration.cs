using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig
{
    public class MaritalStatusConfiguration : IEntityTypeConfiguration<MaritalStatus>
    {
        public void Configure(EntityTypeBuilder<MaritalStatus> builder)
        {
            string table_name = "MaritalStatus";
            builder.ToTable(table_name, ApplicationDbContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(x => x.Id).HasColumnName("Code");
            builder.Property(b => b.Name).HasColumnName("MaritalStatus");
        }
    }
}
