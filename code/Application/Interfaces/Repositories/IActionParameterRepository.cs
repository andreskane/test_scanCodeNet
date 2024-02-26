using Domain.Entities.RulesAggregate;

namespace Application.Interfaces.Repositories
{
    public interface IActionParameterRepository : IGenericRepositoryAsync<ActionParameter>
    {
        Task<IList<ActionParameter>> GetActionParameterByRuleActionId(Int64 ruleActionId, CancellationToken cancellationToken);
    }
}
