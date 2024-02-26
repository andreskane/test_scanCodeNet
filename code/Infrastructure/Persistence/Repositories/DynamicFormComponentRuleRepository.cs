using Application.Dto.Params.DynamicFormItem;
using Application.Interfaces.Repositories;
using ConnectureOS.Framework.Caching.MemCache;
using Domain.Entities.DynamicFormAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class DynamicFormComponentRuleRepository : GenericRepositoryAsync<DynamicFormComponentRule>, IDynamicFormComponentRuleRepository
    {
        public readonly ApplicationDbContext _dataContext;
        private readonly ICacheStore _cache;

        public DynamicFormComponentRuleRepository(ApplicationDbContext dbContext, ICacheStore cache) : base(dbContext)
        {
            _dataContext = dbContext;
            _cache = cache;
        }

        private IQueryable<DynamicFormComponentRule> UntrackedSet() => _dataContext.Set<DynamicFormComponentRule>().AsNoTracking();
        public async Task<IList<DynamicFormComponentRule>> GetFilteredAsync(FilterDynamicFormComponent filter, CancellationToken cancellationToken)
        {
            var query = _dataContext.Set<DynamicFormComponentRule>()
                .Include(x => x.DynamicFormItem).ThenInclude(x => x.DynamicForm)

                .AsQueryable().AsNoTracking();

            if (filter.ComponentName != null)
                query = query.Where(x => x.ComponentName == filter.ComponentName);

            if (filter.DynamicFormName != null)
                query = query.Where(x => x.DynamicFormItem.DynamicForm.Name == filter.DynamicFormName);

            return await query.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task BulkAsync(List<DynamicFormComponentRule> dynamicFormComponentRules)
        {
            _dataContext.DynamicFormComponentRule.AddRange(dynamicFormComponentRules);
            await _dataContext.SaveChangesAsync();
        }

        public async Task BulkDeleteByDynamicFormIdsAsync(List<Int64> dynamicFormIds)
        {
            var rulesToDelete = await _dataContext.DynamicFormComponentRule
                .Where(x => dynamicFormIds.Contains(x.DynamicFormItemId))
                .ToListAsync();

            if (rulesToDelete.Any())
            {
                _dataContext.DynamicFormComponentRule.RemoveRange(rulesToDelete);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<IList<DynamicFormComponentRule>> GetComponentsForBulkByBulkIdAsync(long bulkId, CancellationToken cancellationToken)
        {

            var query = UntrackedSet();


            query = query
                //.Include(x=> x.DynamicFormItem).ThenInclude(x => x.DynamicForm)
                .Include(x => x.DynamicFormItem).ThenInclude(x => x.DynamicForm).ThenInclude(x => x.DynamicFormBulkProcess).ThenInclude(x => x.BulkProcess)
                .Where(x => x.DynamicFormItem.DynamicForm.DynamicFormBulkProcess.Any(x => x.BulkProcessId == bulkId));


            return await query.AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
