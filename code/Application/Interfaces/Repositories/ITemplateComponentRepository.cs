using Application.Dto.Params;
using Domain.Entities.DynamicFormAggregate;

namespace Application.Interfaces.Repositories
{
    public interface ITemplateComponentRepository
    {
        Task<IList<TemplateComponent>> GetFilteredAsync(FilterTemplateComponentsDto filter, CancellationToken cancellationToken);
    }
}
