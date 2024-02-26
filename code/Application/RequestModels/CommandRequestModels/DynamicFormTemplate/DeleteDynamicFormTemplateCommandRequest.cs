using Application.ResponseModels.CommandResponseModels;
using ConnectureOS.Framework.Message;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace Application.RequestModels.CommandRequestModels
{
    public class DeleteDynamicFormTemplateCommandRequest : IRequest<DeleteDynamicFormTemplateCommandResponse>
    {
        [DataMember]
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public Int64 Id { get; set; }
    }
}
