using Application.ResponseModels.QueriesResponseModels;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Application.RequestModels.QueriesRequestModels
{
    public class GetDynamicFormItemByIdQueryRequest : IRequest<GetDynamicFormItemByIdQueryResponse>
    {
        [DataMember]
        [Required(ErrorMessage = "WorkflowDynamicForm Id is required.")]
        public Int64 Id { get; set; }
    }
}
