using Application.Dto;
using Application.RequestModels.Extensions;

namespace Application.ResponseModels.QueriesResponseModels.Components
{
    public class GetTemplateComponentsQueryResponse
    {
        public PaginatedList<TemplateComponentDto> TemplateComponents { get; set; }
    }
}
