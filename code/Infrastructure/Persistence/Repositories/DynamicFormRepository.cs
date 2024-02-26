using Application.Dto.Params.DynamicForm;
using Application.Interfaces.Repositories;
using ConnectureOS.Framework.Caching.MemCache;
using Domain.Entities.DynamicFormAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Infrastructure.Persistence.Repositories
{
    public class DynamicFormRepository : GenericRepositoryAsync<DynamicForm>, IDynamicFormRepository
    {
        public readonly ApplicationDbContext _dataContext;
        private readonly ICacheStore _cache;

        public DynamicFormRepository(ApplicationDbContext dbContext, ICacheStore cache) : base(dbContext)
        {
            _dataContext = dbContext;
            _cache = cache;
        }
        private IQueryable<DynamicForm> UntrackedSet() =>
           _dataContext.DynamicFormDbSet.AsNoTracking();

        public async Task<IList<DynamicForm>> GetDynamicFormByBulkId(long id, CancellationToken cancelationToken)
        {
            return UntrackedSet().Include(p => p.DynamicFormBulkProcess)
                .Where(BulkProcess => BulkProcess.DynamicFormBulkProcess.Any(BulkProcess => BulkProcess.BulkProcessId == id)).ToList();

        }

        public async Task<IList<DynamicForm>> GetFilteredAsync(FilterDynamicForm filter, CancellationToken cancellationToken)
        {
            var query = _dataContext.Set<DynamicForm>().AsQueryable().AsNoTracking();

            if (filter.Name != null)
            {
                query = query.Where(x => x.Name.Contains(filter.Name));
            }

            if (filter.PlanID != null)
            {
                query = query.Where(x => x.PlanId == filter.PlanID);
            }

            if (filter.State != null)
            {
                query = query.Where(x => x.State == filter.State);
            }


            query = query.Include(x => x.FlowList);

            if (filter.State != null)
            {
                query = query.Where(x => x.State == filter.State);
            }

            if (filter.Version != null)
            {
                query = query.Where(x => x.Version == filter.Version);
            }

            //var count = await query.CountAsync(cancellationToken);


            // Apply pagination
            //query = query.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageIndex);

            //var workflows = await query.ToListAsync(cancellationToken);
            return await query.AsNoTracking().ToListAsync(cancellationToken);

            // return workflows;
        }




    }
}
