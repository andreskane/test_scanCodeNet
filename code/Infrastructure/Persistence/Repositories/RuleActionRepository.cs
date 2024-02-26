using Application.Interfaces.Repositories;
using ConnectureOS.Framework.Caching.MemCache;
using Domain.Entities.RulesAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class RuleActionRepository : GenericRepositoryAsync<RuleAction>, IRuleActionRepository
    {
        public readonly ApplicationDbContext _dataContext;
        private readonly ICacheStore _cache;

        public RuleActionRepository(ApplicationDbContext dbContext, ICacheStore cache) : base(dbContext)
        {
            _dataContext = dbContext;
            _cache = cache;
        }

        public async Task<IList<RuleAction>> GetRuleActionsByRuleId(Int64 ruleId, CancellationToken cancellationToken)
        {
            return await _dataContext.Set<RuleAction>().Include(x => x.Parameters).Where(x => x.RuleId == ruleId).AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
