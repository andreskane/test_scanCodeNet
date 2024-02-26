using Application.Interfaces.Repositories;
using ConnectureOS.Framework.Caching.MemCache;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class BulckComponentRepository : GenericRepositoryAsync<BulckComponent>, IBulckComponentRepository
    {
        public readonly ApplicationDbContext _dataContext;
        private readonly ICacheStore _cache;
        public BulckComponentRepository(ApplicationDbContext dbContext, ICacheStore cache) : base(dbContext)
        {
            _dataContext = dbContext;
            _cache = cache;
        }


        public async Task<IList<BulckComponent>> GetBulkComponentByBulckId(long BulckId, CancellationToken cancellationToken)
        {
            return await _dataContext.Set<BulckComponent>().Where(x => x.BulkProcessId == BulckId).OrderBy(x => x.Order).AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
