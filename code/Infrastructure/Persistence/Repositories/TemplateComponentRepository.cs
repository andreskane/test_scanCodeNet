using Application.Dto.Params;
using Application.Interfaces.Repositories;
using ConnectureOS.Framework.Caching.MemCache;
using Domain.Entities.DynamicFormAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class TemplateComponentRepository : ITemplateComponentRepository
    {
        public readonly ApplicationDbContext _dataContext;
        private readonly ICacheStore _cache;
        public TemplateComponentRepository(ApplicationDbContext dbContext)
        {
            _dataContext = dbContext;
        }
        public TemplateComponentRepository(ApplicationDbContext dbContext, ICacheStore cache)
        {
            _dataContext = dbContext;
            _cache = cache;
        }
        private IQueryable<TemplateComponent> UntrackedSet() => _dataContext.Set<TemplateComponent>().AsNoTracking();
        public IEnumerable<TemplateComponent> GetAll()
        {
            var result = UntrackedSet();
            return (IEnumerable<TemplateComponent>)result;
        }
        public async Task<IList<TemplateComponent>> GetFilteredAsync(FilterTemplateComponentsDto filter, CancellationToken cancellationToken)
        {
            var query = UntrackedSet();

            if (filter.DataType != null)
                query = query.Where(x => x.DataType.Equals(filter.DataType));

            return await query.AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
