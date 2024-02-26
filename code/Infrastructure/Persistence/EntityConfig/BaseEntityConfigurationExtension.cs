using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfig
{
    internal static class BaseEntityConfigurationExtension
    {
        internal static void AddBaseConfiguration<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : BaseEntity
        {
            builder.HasKey(x => x.Id);


        }
    }
}
