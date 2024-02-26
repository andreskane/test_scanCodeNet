using Application.Dto.Params;
using Application.Interfaces.Repositories;
using ConnectureOS.Framework.Caching.MemCache;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class RuleRepository : GenericRepositoryAsync<Domain.Entities.RulesAggregate.RuleDynamic>, IRuleRepository
    {
        public readonly ApplicationDbContext _dataContext;
        private readonly ICacheStore _cache;

        public RuleRepository(ApplicationDbContext dbContext, ICacheStore cache) : base(dbContext)
        {
            _dataContext = dbContext;
            _cache = cache;
        }

        public async Task<IList<Domain.Entities.RulesAggregate.RuleDynamic>> GetFilteredAsync(PaginatedRequestDto filter, CancellationToken cancellationToken)
        {
            var query = _dataContext.Set<Domain.Entities.RulesAggregate.RuleDynamic>().AsQueryable().AsNoTracking();

            query = query.Include(x => x.Actions).Include(x => x.DynamicFormRules);

            return await query.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Domain.Entities.RulesAggregate.RuleDynamic> GetRuleById(Int64 Id, CancellationToken cancellationToken)
        {
            return await _dataContext.RulesDbSet.Include(x => x.Actions).ThenInclude(x => x.Parameters).Where(x => x.Id == Id).AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }


        public async Task SetDocumentKey(Int64 Id, string KeyDocument, CancellationToken cancellationToken)
        {

            var entity = _dataContext.Entry(new Domain.Entities.RulesAggregate.RuleDynamic { Id = Id });
            entity.Property(x => x.KeyDocument).CurrentValue = KeyDocument;
            entity.Property(x => x.KeyDocument).IsModified = true;
            await _dataContext.SaveChangesAsync(cancellationToken);
        }



    }


}
