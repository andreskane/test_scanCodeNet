using Application.Dto.Params.DynamicFormItem;
using Application.ResponseModels.QueriesResponseModels.DynamicFormItem;
using MediatR;
using System.Runtime.Serialization;

namespace Application.RequestModels.QueriesRequestModels.DynamicFormItem
{
    public class GetDynamicFormComponentByFiltersQueryRequest : IRequest<GetDynamicFormComponentByFiltersQueryResponse>
    {
        [DataMember]

        public FilterDynamicFormComponent Filter { get; set; }
    }
}
