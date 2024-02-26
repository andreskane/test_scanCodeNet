using Application.ResponseModels.CommandResponseModels.Rules;
using ConnectureOS.Framework.Message;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Application.RequestModels.CommandRequestModels.Rule
{
    public class UpdateEnabledRuleCommandRequest : IRequest<UpdateEnabledRuleCommandResponse>
    {
        [DataMember]
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public Int64 RuleId { get; set; }
        public bool Enabled { get; set; }
    }
}
