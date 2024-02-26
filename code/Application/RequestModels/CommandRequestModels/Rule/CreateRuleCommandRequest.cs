using Application.Dto;
using Application.ResponseModels.CommandResponseModels.Rules;
using MediatR;

namespace Application.RequestModels.CommandRequestModels.Rule
{
    public class CreateRuleCommandRequest : IRequest<CreateRuleCommandResponse>
    {
        public RuleDto Rule { get; set; }
    }
}
