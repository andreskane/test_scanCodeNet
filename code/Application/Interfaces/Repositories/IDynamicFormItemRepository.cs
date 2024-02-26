using Domain.Entities.DynamicFormAggregate;

namespace Application.Interfaces.Repositories
{
    public interface IDynamicFormItemRepository : IGenericRepositoryAsync<DynamicFormItem>
    {
        Task<DynamicFormItem> GetLastPublishedWorkflowDynamicForm(long productId, string zipCodeId, CancellationToken cancellationToken);
        Task<DynamicFormItem> GetDynamicByWorkflowID(long Id, CancellationToken cancellationToken);
        Task<DynamicFormItem> GetDynamicFormItemByIdWithDynamicForm(long Id, CancellationToken cancellationToken);
        Task<IList<DynamicFormItem>> GetDynamicFormItemByDynamicFormId(Int64 dynamicFormId, CancellationToken cancellationToken);
        Task<IList<DynamicFormItem>> GetDynamicFormItemsByBulkId(long BulkId, CancellationToken cancellationToken);
        Task<IList<DynamicFormItem>> GetByTemplateId(long templateId, CancellationToken cancellationToken);

    }
}
