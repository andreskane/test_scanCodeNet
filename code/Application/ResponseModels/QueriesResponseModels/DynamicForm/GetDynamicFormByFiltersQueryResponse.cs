using Application.Dto;
using Application.RequestModels.Extensions;

namespace Application.ResponseModels.QueriesResponseModels
{
    public class GetDynamicFormByFiltersQueryResponse
    {
        public PaginatedList<DynamicFormDto> Workflows { get; set; }
    }
}
