using Application.ResponseModels.QueriesResponseModels.Rule;
using MediatR;

namespace Application.RequestModels.QueriesRequestModels.Rule
{
    public class GetRuleByIdQueryRequest : IRequest<GetRuleByIdQueryResponse>
    {
        public Int64 Id { get; set; }
    }
}
