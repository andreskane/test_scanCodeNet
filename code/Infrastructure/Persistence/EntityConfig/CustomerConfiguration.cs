using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {

        public void Configure(EntityTypeBuilder<Customer> builder)
        {

            string table_name = "Customer";
            builder.ToTable(table_name, ApplicationDbContext.DEFAULT_SCHEMA);

            builder.HasKey(cr => cr.SocialSecurity);

            builder.Property(x => x.SocialSecurity).HasColumnName("SocialSecurity");
            builder.Property(x => x.FirstName).HasColumnName("FirstName");
            builder.Property(x => x.SecondName).HasColumnName("SecondName");
            builder.Property(x => x.FirtsSurname).HasColumnName("FirtsSurname");
            builder.Property(x => x.SecondSurname).HasColumnName("SecondSurname");
            builder.Property(x => x.ThirdSurname).HasColumnName("ThirdSurname");
            builder.Property(x => x.FullName).HasColumnName("FullName");
            builder.Property<string>("_status").UsePropertyAccessMode(PropertyAccessMode.Field)
              .HasColumnName("Status");
            builder.HasOne(p => p.Status).WithMany().HasForeignKey("_status");


            builder.Property(x => x.LastAccess).HasColumnName("LastAccess");

            builder.Ignore(x => x.Id);
        }
    }
}
