using Application.ResponseModels.QueriesResponseModels;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Application.RequestModels.QueriesRequestModels
{
    public class GetDynamicFormProductAttributesByIdQueryRequest : IRequest<GetDynamicFormProductAttributesByIdQueryResponse>
    {
        [DataMember]
        [Required(ErrorMessage = "WorkflowProductAttribute Id is required.")]
        public Int64 Id { get; set; }
    }
}
