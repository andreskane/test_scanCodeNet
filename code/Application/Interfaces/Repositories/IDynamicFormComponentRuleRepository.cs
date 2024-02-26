using Application.Dto.Params.DynamicFormItem;
using Domain.Entities.DynamicFormAggregate;

namespace Application.Interfaces.Repositories
{
    public interface IDynamicFormComponentRuleRepository : IGenericRepositoryAsync<DynamicFormComponentRule>
    {
        Task<IList<DynamicFormComponentRule>> GetFilteredAsync(FilterDynamicFormComponent filter, CancellationToken cancellationToken);

        Task BulkAsync(List<DynamicFormComponentRule> dynamicFormComponentRules);

        Task BulkDeleteByDynamicFormIdsAsync(List<Int64> dynamicFormIds);
        Task<IList<DynamicFormComponentRule>> GetComponentsForBulkByBulkIdAsync(long bulkId, CancellationToken cancellationToken);
    }
}
