using Domain.Entities.DynamicFormAggregate;

namespace Application.Interfaces.Repositories
{
    public interface IDynamicFormPlanRepository : IGenericRepositoryAsync<DynamicFormPlan>
    {
        Task<DynamicFormPlan> GetPublishedPlanAsync(int dynamicFormId);
    }
}
