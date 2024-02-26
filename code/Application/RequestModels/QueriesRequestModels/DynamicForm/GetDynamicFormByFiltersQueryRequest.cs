using Application.Dto.Params.DynamicForm;
using Application.ResponseModels.QueriesResponseModels;
using MediatR;
using System.Runtime.Serialization;

namespace Application.RequestModels.QueriesRequestModels
{
    public class GetDynamicFormByFiltersQueryRequest : IRequest<GetDynamicFormByFiltersQueryResponse>
    {
        [DataMember]

        public FilterDynamicForm Filter { get; set; }
    }
}
