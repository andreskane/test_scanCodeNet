using Application.Dto.Params.ListValue;
using Application.ResponseModels.QueriesResponseModels.ListValue;
using MediatR;

namespace Application.RequestModels.QueriesRequestModels.ListValue
{
    public class GetListRowQueryRequest : IRequest<GetListRowQueryResponse>
    {
        public ListValueFilter Filter { get; set; }
    }
}
