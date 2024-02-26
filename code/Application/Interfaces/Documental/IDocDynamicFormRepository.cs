using Domain.Entities.Layout;


namespace Application.Interfaces.Documental
{
    public interface IDocDynamicFormRepository
    {
        Task<string> InsertDynamicForm(RootObject layout);
        Task<string> UpdateDynamicForm(RootObject layout, String KeyDocument);
        Task<RootObject> GetDynamicFormByKey(String KeyDocument);
        Task<string> DuplicateDynamicForm(String KeyDocument);


        Task<string> InsertTemplate(DocLayoutTemplate layout);
        Task<string> UpdateTemplate(DocLayoutTemplate layout, String KeyDocument);
        Task<DocLayoutTemplate> GetTemplateByKey(String KeyDocument);

        Task<List<RootObject>> GetIDDocumentsByIDTemplate(Int64 templateID);


    }
}