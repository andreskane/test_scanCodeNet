using Application.Dto.Params;
using Application.ResponseModels.QueriesResponseModels.Components;
using MediatR;
using System.Runtime.Serialization;

namespace Application.RequestModels.QueriesRequestModels.Components
{
    public class GetTemplateComponentsQueryRequest : IRequest<GetTemplateComponentsQueryResponse>
    {
        [DataMember]
        public FilterTemplateComponentsDto Filter { get; set; }
    }
}
