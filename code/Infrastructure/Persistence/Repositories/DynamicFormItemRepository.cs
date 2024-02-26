using Application.Interfaces.Repositories;
using ConnectureOS.Framework.Caching.MemCache;
using Domain.Entities.DynamicFormAggregate;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class DynamicFormItemRepository : GenericRepositoryAsync<DynamicFormItem>, IDynamicFormItemRepository
    {
        public readonly ApplicationDbContext _dataContext;
        private readonly ICacheStore _cache;

        public DynamicFormItemRepository(ApplicationDbContext dbContext, ICacheStore cache) : base(dbContext)
        {
            _dataContext = dbContext;
            _cache = cache;
        }
        private IQueryable<DynamicFormItem> UntrackedSet() =>
          _dataContext.DynamicFormItemDbSet.AsNoTracking();
        public async Task<DynamicFormItem> GetDynamicByWorkflowID(long Id, CancellationToken cancellationToken)
        {
            return await _dataContext.DynamicFormItemDbSet.Where(x => x.Id == Id)
                   .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<DynamicFormItem> GetDynamicFormItemByIdWithDynamicForm(long Id, CancellationToken cancellationToken)
        {
            return await _dataContext.DynamicFormItemDbSet.Include(x => x.DynamicForm).Where(x => x.Id == Id)
                   .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IList<DynamicFormItem>> GetDynamicFormItemsByBulkId(long BulkId, CancellationToken cancellationToken)
        {

            var query = UntrackedSet().Include(p => p.DynamicForm).ThenInclude(p => p.DynamicFormBulkProcess)
               .Where(BulkProcess => BulkProcess.DynamicForm.DynamicFormBulkProcess.Any(BulkProcess => BulkProcess.BulkProcessId == BulkId))
               .Where(x => x.Status == DynamicFormStatusEnum.Published).ToList();

            return query;
        }

        public async Task<DynamicFormItem> GetLastPublishedWorkflowDynamicForm(long productId, string zipCodeId, CancellationToken cancellationToken)
        {
            var DynamicFlow = await _dataContext.DynamicFormDbSet.Where(x => x.PlanId == productId).AsQueryable().AsNoTracking().FirstOrDefaultAsync(cancellationToken);

            if (DynamicFlow == null)
                return new DynamicFormItem();

            var flow = await _dataContext.DynamicFormItemDbSet.Where(x => x.DynamicFormId == DynamicFlow.Id && x.Status == DynamicFormStatusEnum.Published).AsQueryable().AsNoTracking()
              .OrderByDescending(x => x.Version)
              .FirstOrDefaultAsync(cancellationToken);

            return flow;

        }

        public async Task<IList<DynamicFormItem>> GetDynamicFormItemByDynamicFormId(Int64 dynamicFormId, CancellationToken cancellationToken)
        {
            var query = UntrackedSet().Include(p => p.DynamicForm).Where(x => x.DynamicFormId == dynamicFormId)
               .Where(x => x.Status == DynamicFormStatusEnum.Published).ToList();

            return query;
        }

        public async Task<IList<DynamicFormItem>> GetByTemplateId(long templateId, CancellationToken cancellationToken)
        {
            var paramToSearch = $"\"templateId\":{templateId}";

            var query = UntrackedSet()
                .Where(x => x.DynamicFormTemplateId == null)
                .Where(x => x.Layout.Contains(paramToSearch))
               .ToList();


            return query;

        }
    }
}
