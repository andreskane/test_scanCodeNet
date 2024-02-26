using Application.Dto.Params;

namespace Application.Interfaces.Repositories
{
    public interface IRuleRepository : IGenericRepositoryAsync<Domain.Entities.RulesAggregate.RuleDynamic>
    {
        Task<IList<Domain.Entities.RulesAggregate.RuleDynamic>> GetFilteredAsync(PaginatedRequestDto filter, CancellationToken cancellationToken);

        Task<Domain.Entities.RulesAggregate.RuleDynamic> GetRuleById(Int64 Id, CancellationToken cancellationToken);
        Task SetDocumentKey(Int64 Id, string KeyDocument, CancellationToken cancellationToken);
    }
}
