using Application.Interfaces.Repositories;
using ConnectureOS.Framework.Caching.MemCache;
using Domain.Entities.DynamicFormAggregate;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class DynamicFormPlanRepository : GenericRepositoryAsync<DynamicFormPlan>, IDynamicFormPlanRepository
    {
        public readonly ApplicationDbContext _context;
        private readonly ICacheStore _cache;

        public DynamicFormPlanRepository(ApplicationDbContext dbContext, ICacheStore cache) : base(dbContext)
        {
            _context = dbContext;
            _cache = cache;
        }


        public async Task<DynamicFormPlan> GetPublishedPlanAsync(int dynamicFormId)
        {
            return await _context.DynamicFormPlan
                .FirstOrDefaultAsync(plan => plan.DynamicFormId == dynamicFormId && plan.Status == DynamicFormStatusEnum.Published);
        }
    }
}
