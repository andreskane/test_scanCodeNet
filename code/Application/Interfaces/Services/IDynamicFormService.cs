using Application.Dto;
using Domain.Entities.DynamicFormAggregate;

namespace Application.Interfaces.Services
{
    public interface IDynamicFormService
    {
        Task<DynamicFormItem> CreateDynamicFormItem(DynamicFormItemDto dynamicForm, CancellationToken cancellationToken);
        Task<DynamicFormItem> UpdateDynamicFormItem(DynamicFormItemDto dynamicForm, CancellationToken cancellationToken);
        Task<DynamicFormItem> CopyDynamicFormItem(DynamicFormItem dynamicForm, CancellationToken cancellationToken);
        Task<DynamicFormItem> SetVersionDynamicFormItem(DynamicFormItem dinamicFormItem, CancellationToken cancellationToken);
    }
}
