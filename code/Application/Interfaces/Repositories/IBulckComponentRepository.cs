using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IBulckComponentRepository : IGenericRepositoryAsync<BulckComponent>
    {
        Task<IList<BulckComponent>> GetBulkComponentByBulckId(Int64 BulckId, CancellationToken cancellationToken);
    }
}