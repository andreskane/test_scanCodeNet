using Application.Dto;
using Application.Services.Rules;
using AutoMapper;
using Domain.Entities.Common;
using Domain.Entities.RulesAggregate;
using drx.cos.workflow.rulemanager.Services.Rules.HelperFunctions;
using MediatR;
using RulesEngine.Models;

namespace drx.cos.workflow.rulemanager.Services.Rules;

public class ValidateRuleHandler : IRequestHandler<ValidateRuleRequest, ValidateRuleResponse>
{
    private readonly IMapper _mapper;
    private readonly ILogger<ValidateRuleHandler> _logger;
    private readonly IProcessRuleEngine _processRuleEngine;

    public ValidateRuleHandler(
        IMapper mapper,
        ILogger<ValidateRuleHandler> logger,

          IProcessRuleEngine processRuleEngine
        )
    {
        _mapper = mapper;
        _logger = logger;


        _processRuleEngine = processRuleEngine;
    }

    public async Task<ValidateRuleResponse> Handle(ValidateRuleRequest request, CancellationToken cancellationToken)
    {
        var response = new ValidateRuleResponse();


        if (request.Rule == null)
        {
            response.Message = "Rule is required";
            return response;
        }

        var rule = new Domain.Entities.RulesAggregate.RuleDynamic();



        rule.Name = request.Rule.Name + DateTime.Now.ToLongDateString;
        rule.Description = request.Rule.Description;
        rule.Enabled = request.Rule.Enabled;
        rule.Version = request.Rule.Version;

        var ruleActionsParam = _mapper.Map<List<Domain.Entities.RulesAggregate.RuleAction>>(request.Rule.Actions).ToList();
        rule.Actions = ruleActionsParam;


        var RootRule = new RootRules();
        RootRule.workflowRules = new List<Workflow>();
        RootRule.AddWorkflowRule(rule);
        var reSettings = new ReSettings
        {
            CustomTypes = new Type[] { typeof(Common) },

        };
        var responseExecute = await _processRuleEngine.Execute(RootRule.workflowRules, rule, request.ParamInput, reSettings, cancellationToken);


        response.ParamOutPut = responseExecute.ParamOutPut;
        response.Message = responseExecute.Message;




        return response;
    }
}

public class ValidateRuleResponse
{
    public List<KeyValue> ParamOutPut { get; set; }

    public string Message { get; set; }
}

public class ValidateRuleRequest : IRequest<ValidateRuleResponse>
{
    public RuleDto Rule { get; set; }
    public List<KeyValue> ParamInput { get; set; }
}