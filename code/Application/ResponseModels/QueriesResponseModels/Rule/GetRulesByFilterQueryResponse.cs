using Application.Dto;
using Application.RequestModels.Extensions;

namespace Application.ResponseModels.QueriesResponseModels.Rule
{
    public class GetRulesByFilterQueryResponse
    {
        public PaginatedList<RuleDto> Rules { get; set; }
    }
}
