using Domain.Entities.RulesAggregate;

namespace Application.Interfaces.Repositories
{
    public interface IRuleActionRepository : IGenericRepositoryAsync<Domain.Entities.RulesAggregate.RuleAction>
    {
        Task<IList<RuleAction>> GetRuleActionsByRuleId(Int64 ruleId, CancellationToken cancellationToken);
    }
}
