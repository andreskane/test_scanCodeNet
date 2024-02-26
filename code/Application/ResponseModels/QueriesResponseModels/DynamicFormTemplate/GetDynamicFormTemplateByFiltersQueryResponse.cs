using Application.Dto;
using Application.RequestModels.Extensions;

namespace Application.ResponseModels.QueriesResponseModels
{
    public class GetDynamicFormTemplateByFiltersQueryResponse
    {
        public PaginatedList<DynamicFormTemplateDto> WorkflowsTemplates { get; set; }
    }
}

