using Application.Dto;
using Application.ResponseModels.CommandResponseModels.Rules;
using MediatR;

namespace Application.RequestModels.CommandRequestModels.Rule
{
    public class UpdateRuleCommandRequest : IRequest<UpdateRuleCommandResponse>
    {
        public RuleDto Rule { get; set; }
    }
}
