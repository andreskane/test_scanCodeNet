using Application.Dto;
using Application.ResponseModels.CommandResponseModels;
using ConnectureOS.Framework.Message;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Application.RequestModels.CommandRequestModels
{
    public class UpdateDynamicFormCommandRequest : IRequest<UpdateDynamicFormCommandResponse>
    {
        [DataMember]
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public DynamicFormDto Workflow { get; set; }
    }
}

