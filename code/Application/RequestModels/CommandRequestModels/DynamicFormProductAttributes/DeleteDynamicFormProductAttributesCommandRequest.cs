using Application.ResponseModels.CommandResponseModel;
using ConnectureOS.Framework.Message;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Application.RequestModels.CommandRequestModels
{
    public class DeleteDynamicFormProductAttributesCommandRequest : IRequest<DeleteDynamicFormProductAttributesCommandResponse>
    {
        [DataMember]
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public Int64 Id { get; set; }
    }
}
