using Application.Interfaces.Repositories;
using ConnectureOS.Framework.Caching.MemCache;
using Domain.Entities.RulesAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ActionParameterRepository : GenericRepositoryAsync<ActionParameter>, IActionParameterRepository
    {
        public readonly ApplicationDbContext _dataContext;
        private readonly ICacheStore _cache;

        public ActionParameterRepository(ApplicationDbContext dbContext, ICacheStore cache) : base(dbContext)
        {
            _dataContext = dbContext;
            _cache = cache;
        }

        public async Task<IList<ActionParameter>> GetActionParameterByRuleActionId(Int64 ruleActionId, CancellationToken cancellationToken)
        {
            return await _dataContext.Set<ActionParameter>().Where(x => x.RuleActtioId == ruleActionId).AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
