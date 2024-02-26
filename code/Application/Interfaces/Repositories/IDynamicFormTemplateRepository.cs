using Application.Dto.Params.DynamicForm;
using Domain.Entities.DynamicFormAggregate;

namespace Application.Interfaces.Repositories
{
    public interface IDynamicFormTemplateRepository : IGenericRepositoryAsync<DynamicFormTemplate>
    {
        Task<IList<DynamicFormTemplate>> GetFilteredAsync(FilterDynamicForm filter, CancellationToken cancellationToken);
        Task<DynamicFormTemplate> GetCompleteByAsync(Int64 Id);
    }
}
