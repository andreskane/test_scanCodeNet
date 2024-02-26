using Application.Dto.Params;
using Application.ResponseModels.QueriesResponseModels.Rule;
using MediatR;
using System.Runtime.Serialization;

namespace Application.RequestModels.QueriesRequestModels.Rule
{
    public class GetRulesByFilterQueryRequest : IRequest<GetRulesByFilterQueryResponse>
    {
        [DataMember]
        public PaginatedRequestDto Filter { get; set; }
    }
}
