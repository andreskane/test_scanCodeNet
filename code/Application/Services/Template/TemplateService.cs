using Application.Interfaces.Documental;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities.DynamicFormAggregate;
using Domain.Entities.Layout;
using Microsoft.Extensions.Logging;

namespace Application.Services.Template;

public interface ITemplateService
{
    Task<Boolean> Execute(Int64 idTemplate, DocLayoutTemplate docLayoutTemplate, CancellationToken cancellationToken);
}
public class TemplateService : ITemplateService
{
    private readonly IMapper _mapper;
    private readonly ILogger<TemplateService> _logger;
    private readonly IDynamicFormItemRepository _dynamicFormItemRepository;
    private readonly IDocDynamicFormRepository _docDynamicFormRepository;
    public TemplateService(
        IMapper mapper,
        ILogger<TemplateService> logger,
        IDynamicFormItemRepository dynamicFormItemRepository,
        IDocDynamicFormRepository docDynamicFormRepository

                          )
    {
        _mapper = mapper;
        _logger = logger;
        _dynamicFormItemRepository = dynamicFormItemRepository;
        _docDynamicFormRepository = docDynamicFormRepository;


    }


    public async Task<Boolean> Execute(Int64 idTemplate, DocLayoutTemplate docLayoutTemplate, CancellationToken cancellationToken)
    {
        Boolean response = false;

       
        var dynamicFormItems = await _dynamicFormItemRepository.GetByTemplateId(idTemplate, cancellationToken);
        



        if (dynamicFormItems == null)
            return response;
        for (int i = 0; i < dynamicFormItems.Count; i++)
        {
           DynamicFormItem? item = dynamicFormItems[i];

            
            if (item.CodeFlow == null)
                continue;
            var layout = await _docDynamicFormRepository.GetDynamicFormByKey(item.CodeFlow);
            var newRootObject = UpdateLayout(idTemplate, docLayoutTemplate, layout);

            await _docDynamicFormRepository.UpdateDynamicForm(newRootObject, item.CodeFlow);

        }


        return response;
    }

    private RootObject UpdateLayout(Int64 idTemplate, DocLayoutTemplate docLayoutTemplate, RootObject rootObject)
    {
        int PageIdToReplace = 0;

        RootObject newRootObject = new RootObject();
        newRootObject.Pages = new List<DocDynamicForm>();

        
        foreach (var page in rootObject.Pages)
        {
             
            foreach (var workflowItem in page.workflowTable)
            {
                if (workflowItem.templateId == idTemplate)
                {
                    PageIdToReplace = page.page;
                    page.workflowTable = docLayoutTemplate.Pages;

                }

            }
            newRootObject.Pages.Add(page);
        }

        
        return newRootObject;

    }




}
