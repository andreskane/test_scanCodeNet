using Application.Dto.Params.DynamicFormItem;
using Application.RequestModels.Extensions;

namespace Application.ResponseModels.QueriesResponseModels.DynamicFormItem
{
    public class GetDynamicFormComponentByFiltersQueryResponse
    {
        public PaginatedList<DynamicFormComponentRuleDto> Component { get; set; }
    }
}
