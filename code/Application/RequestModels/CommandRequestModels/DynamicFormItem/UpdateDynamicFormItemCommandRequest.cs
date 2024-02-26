using Application.Dto;
using Application.ResponseModels.CommandResponseModels;
using ConnectureOS.Framework.Message;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Application.RequestModels.CommandRequestModels
{
    public class UpdateDynamicFormItemCommandRequest : IRequest<UpdateDynamicFormItemCommandResponse>
    {
        [DataMember]
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public DynamicFormItemDto WDynamicForm { get; set; }
    }
}
