using Domain.Entities.Layout;

namespace Application.Interfaces.Services
{
    public interface IFormComponentRuleService
    {
        Task GenerateFormComponentList(List<DocDynamicForm> pages, Int64 dinamicFormItemId);
    }
}
