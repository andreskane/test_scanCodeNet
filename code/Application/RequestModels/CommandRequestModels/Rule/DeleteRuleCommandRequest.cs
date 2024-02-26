using Application.ResponseModels.CommandResponseModels.Rules;
using ConnectureOS.Framework.Message;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Application.RequestModels.CommandRequestModels.Rule
{
    public class DeleteRuleCommandRequest : IRequest<DeleteRuleCommandResponse>
    {
        [DataMember]
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public Int64 Id { get; set; }
    }
}
