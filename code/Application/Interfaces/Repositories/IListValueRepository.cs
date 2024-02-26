using Application.Dto.Params.ListValue;
using Domain.Entities.ListAggregate;

namespace Application.Interfaces.Repositories
{
    public interface IListValueRepository : IGenericRepositoryAsync<ListValue>
    {
        Task<IList<ListValue>> GetFilteredAsync(ListValueFilter filter, CancellationToken cancellationToken);
        Task<IList<ListValue>> GetByListIdAsync(Int64 listId, CancellationToken cancellationToken);
        Task<IList<ListValue>> GetByListNameAsync(String ListName, CancellationToken cancellationToken);
        Task<IList<ListValue>> GetByListNameAsync(String ListName, String Key, CancellationToken cancellationToken);
        Task<IList<ListValue>> GetKeyByListNameAsync(String ListName, String Value, CancellationToken cancellationToken);

        Task<IEnumerable<ListDefinition>> GetAllListDefinitionAsync();
        Task<List<ListValue>> AddRangeListValueAsync(List<ListValue> entity, CancellationToken cancellationToken = default);
        Task<int> DeleteListValueAsync(ListValue entity, CancellationToken cancellationToken = default);

        Task<ListValue> UpdateListValueAsync(ListValue entity, CancellationToken cancellationToken);
    }
}
