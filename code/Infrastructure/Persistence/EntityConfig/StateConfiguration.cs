using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig
{
    public class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            string table_name = "State";
            builder.ToTable(table_name, ApplicationDbContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(x => x.Id).HasColumnName("Code");
            builder.Property(b => b.Name).HasColumnName("State");
        }
    }
}
