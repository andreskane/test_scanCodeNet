using Application.Dto.Params.DynamicForm;
using Application.Interfaces.Repositories;
using ConnectureOS.Framework.Caching.MemCache;
using Domain.Entities.DynamicFormAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class DynamicFormTemplateRepository : GenericRepositoryAsync<DynamicFormTemplate>, IDynamicFormTemplateRepository
    {
        public readonly ApplicationDbContext _dataContext;
        private readonly ICacheStore _cache;

        public DynamicFormTemplateRepository(ApplicationDbContext dbContext, ICacheStore cache) : base(dbContext)
        {
            _dataContext = dbContext;
            _cache = cache;
        }

        public async Task<IList<DynamicFormTemplate>> GetFilteredAsync(FilterDynamicForm filter, CancellationToken cancellationToken)
        {
            var query = _dataContext.Set<DynamicFormTemplate>().AsQueryable().AsNoTracking();


            query = query.Include(x => x.FlowList);

            //if (filter.Id != null)
            //{
            //    query = query.Where(x => x.Id == filter.Id);
            //}

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

        public async Task<DynamicFormTemplate> GetCompleteByAsync(Int64 Id)
        {
            var query = _dataContext.Set<DynamicFormTemplate>().AsQueryable().AsNoTracking();


            query = query.Include(x => x.FlowList);

            query = query.Where(x => x.Id == Id);



            return query.AsNoTracking().FirstOrDefault();
        }



    }
}
