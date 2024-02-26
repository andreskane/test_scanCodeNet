using Domain.Entities.RulesAggregate;

namespace Application.Interfaces.Documental
{
    public interface IDocRulesRepository
    {

        Task<RootRules> GetDynamicFormByKey(string KeyDocument);
        Task<string> InsertDynamicForm(RootRules rootRules);
        Task<string> UpdateDynamicForm(RootRules rootRules, String KeyDocument);
    }
}
