using Application.Dto.Params.DynamicForm;
using Domain.Entities.DynamicFormAggregate;

namespace Application.Interfaces.Repositories
{
    public interface IDynamicFormRepository : IGenericRepositoryAsync<DynamicForm>
    {
        Task<IList<DynamicForm>> GetDynamicFormByBulkId(long id, CancellationToken cancelationToken);
        Task<IList<DynamicForm>> GetFilteredAsync(FilterDynamicForm filter, CancellationToken cancellationToken);
    }
}
