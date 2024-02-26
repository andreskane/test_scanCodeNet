using Application.Dto;
using Application.Interfaces.Documental;
using Application.Interfaces.Repositories;
using Application.RequestModels.CommandRequestModels.Rule;
using Application.ResponseModels.CommandResponseModels.Rules;
using AutoMapper;
using Domain.Entities.RulesAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using RulesEngine.Models;

namespace Application.Handlers.CommandHandlers.Rule
{
    public class CreateRuleCommandHandler : IRequestHandler<CreateRuleCommandRequest, CreateRuleCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateRuleCommandHandler> _logger;
        private readonly IGenericRepositoryAsync<Domain.Entities.RulesAggregate.RuleDynamic> _ruleRepository;
        private readonly IGenericRepositoryAsync<Domain.Entities.RulesAggregate.RuleAction> _ruleActionRepository;
        private readonly IActionParameterRepository _actionParameterRepository;
        private readonly IDocRulesRepository _docRulesRepository;
        public CreateRuleCommandHandler(IMapper mapper,
            IGenericRepositoryAsync<Domain.Entities.RulesAggregate.RuleDynamic> ruleRepository,
            IGenericRepositoryAsync<Domain.Entities.RulesAggregate.RuleAction> ruleActionRepository,
            IActionParameterRepository actionParameterRepository,
            IDocRulesRepository docRulesRepository,
            ILogger<CreateRuleCommandHandler> logger)
        {
            _mapper = mapper;
            _ruleRepository = ruleRepository;
            _ruleActionRepository = ruleActionRepository;
            _actionParameterRepository = actionParameterRepository;
            _logger = logger;
            _docRulesRepository = docRulesRepository;
        }

        public async Task<CreateRuleCommandResponse> Handle(CreateRuleCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new CreateRuleCommandResponse();

                var rule = new Domain.Entities.RulesAggregate.RuleDynamic();

                rule.Name = request.Rule.Name;
                rule.Description = request.Rule.Description;
                rule.Enabled = request.Rule.Enabled;
                rule.Version = request.Rule.Version;



                await _ruleRepository.AddAsync(rule, cancellationToken);

                response.Rule = _mapper.Map<RuleDto>(rule);

                foreach (var action in request.Rule.Actions)
                {
                    var ruleAction = _mapper.Map<Domain.Entities.RulesAggregate.RuleAction>(action);
                    ruleAction.RuleId = rule.Id;
                    await _ruleActionRepository.AddAsync(ruleAction, cancellationToken);

                    var ruleActionDto = _mapper.Map<RuleActionDto>(ruleAction);


                    response.Rule.Actions.Add(ruleActionDto);
                }

 
                var RootRule = new RootRules();
                RootRule.workflowRules = new List<Workflow>();
                RootRule.AddWorkflowRule(rule);

                var key = await _docRulesRepository.InsertDynamicForm(RootRule);
                rule.KeyDocument = key;
                await _ruleRepository.UpdateAsync(rule, cancellationToken);



                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                throw ex;
            }
        }
    }
}
