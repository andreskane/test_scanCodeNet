using Application.ResponseModels.QueriesResponseModels;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Application.RequestModels.QueriesRequestModels
{
    public class GetDynamicFormTemplateByIdQueryRequest : IRequest<GetDynamicFormTemplateByIdQueryResponse>
    {
        [DataMember]
        [Required(ErrorMessage = "Workflow Id is required.")]
        public Int64 Id { get; set; }
    }
}
