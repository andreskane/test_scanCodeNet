using Application.ResponseModels.CommandResponseModels;
using ConnectureOS.Framework.Message;
using Domain.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Application.RequestModels.CommandRequestModels
{
    public class CreateDynamicFormCommandRequest : IRequest<CreateDynamicFormCommandResponse>
    {
        [DataMember]
        [Required(ErrorMessage = ErrorMessageText.Required)]
        //  public WorkflowDto Workflow { get; set; }
        public string Name { get; set; }
        [DataMember]

        public string Description { get; set; }
        [DataMember]

        public DynamicFormStatusEnum State { get; set; }
        [DataMember]
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public Int64 PlanId { get; set; }
        //[DataMember]

        //public Int32 Version { get; set; }
    }
}
