using Application.Interfaces.Repositories;
using ConnectureOS.Framework.Caching.MemCache;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class BulkRepository : GenericRepositoryAsync<BulkProcess>, IBulkRepository
    {
        public readonly ApplicationDbContext _dataContext;
        private readonly ICacheStore _cache;
        public BulkRepository(ApplicationDbContext dbContext, ICacheStore cache) : base(dbContext)
        {
            _dataContext = dbContext;
            _cache = cache;
        }
        private IQueryable<BulkProcess> UntrackedSet() => _dataContext.Set<BulkProcess>().AsNoTracking();
        public IEnumerable<BulkProcess> GetAll()
        {

            var result = UntrackedSet();


            return (IEnumerable<BulkProcess>)result;


        }

        public async Task<BulkProcess> GetBulkProcessByIdAsync(long BulkProcessId, CancellationToken cancellationToken)
        {
            return await _dataContext.Set<BulkProcess>().Where(x => x.Id == BulkProcessId).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<IList<BulkProcess>> GetBulkProcessesAsyncExcludeDraft(CancellationToken cancellationToken)
        {
            var res = await _dataContext.Set<BulkProcess>().Where(x => x.Status != ProcessStatusEnum.Draft).AsNoTracking().ToListAsync(cancellationToken);

            if (res == null)
            {
                return new List<BulkProcess>();
            }
            return res;

        }
        public async Task AsignDynamicForms(long BulckId, List<Int64> ListIds, CancellationToken cancellationToken)
        {

            var objetosCToRemove = _dataContext.Set<DynamicFormBulckProcess>().Where(c => c.BulkProcessId == BulckId).ToList();

            if (objetosCToRemove.Any())
            {
                _dataContext.Set<DynamicFormBulckProcess>().RemoveRange(objetosCToRemove);
                await _dataContext.SaveChangesAsync(cancellationToken);
            }

            var bulk = _dataContext.Set<BulkProcess>().Where(c => c.Id == BulckId).FirstOrDefault();

            foreach (var item in ListIds)
            {
                var bulkComponent = new DynamicFormBulckProcess()
                {

                    BulkProcessId = bulk.Id,
                    DynamicFormId = item,

                };

                await _dataContext.Set<DynamicFormBulckProcess>().AddAsync(bulkComponent);
            }
            await _dataContext.SaveChangesAsync();

        }

        public async Task<IList<BulkProcess>> GetBulkProcessesAsyncByStatus(ProcessStatusEnum Status, CancellationToken cancellationToken)
        {
            var res = await _dataContext.Set<BulkProcess>().Where(x => x.Status == Status).AsNoTracking().ToListAsync(cancellationToken);

            if (res == null)
            {
                return new List<BulkProcess>();
            }
            return res;

        }

        public async Task<IList<BulckComponent>> GetBulkComponentsByBulkProcessId(Int64 BulkProcessId, CancellationToken cancellationToken)
        {
            var components = await _dataContext.Set<BulckComponent>().Where(x => x.BulkProcessId == BulkProcessId).AsNoTracking().ToListAsync(cancellationToken);

            if (components == null)
            {
                return new List<BulckComponent>();
            }

            return components;
        }
    }
}
