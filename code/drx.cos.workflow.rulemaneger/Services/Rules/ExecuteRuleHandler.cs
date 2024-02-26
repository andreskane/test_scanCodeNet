using Application.Interfaces.Documental;
using Application.Interfaces.Repositories;
using Application.Services.Rules;
using Domain.Entities.Common;
using drx.cos.workflow.rulemanager.Services.Rules.HelperFunctions;
using MediatR;
using RulesEngine.Models;


namespace drx.cos.workflow.rulemanager.Services.Rules;


public class ExecuteRuleResponse
{
    public List<KeyValue> ParamOutPut { get; set; }

    public string Message { get; set; }
}

public class ExecuteRuleRequest : IRequest<ExecuteRuleResponse>
{

    public Int64 RuleId { get; set; }

    public List<KeyValue> ParamInput { get; set; }

}

public class ExecuteRuleHandler : IRequestHandler<ExecuteRuleRequest, ExecuteRuleResponse>
{

    private readonly ILogger<ExecuteRuleHandler> _logger;
    private readonly IRuleRepository _ruleRepository;
    private readonly IDocRulesRepository _docRulesRepository;
    private readonly IProcessRuleEngine _processRuleEngine;

    public ExecuteRuleHandler(

        ILogger<ExecuteRuleHandler> logger,
        IDocRulesRepository docRulesRepository,
        IRuleRepository ruleRepository,
        IProcessRuleEngine processRuleEngine

        )
    {

        _logger = logger;
        _docRulesRepository = docRulesRepository;
        _ruleRepository = ruleRepository;
        _processRuleEngine = processRuleEngine;
    }

    public async Task<ExecuteRuleResponse> Handle(ExecuteRuleRequest request, CancellationToken cancellationToken)
    {
        var response = new ExecuteRuleResponse();

        if (request.RuleId == 0)
        {
            response.Message = "RuleId is required";
            return response;
        }

        var rule = await _ruleRepository.GetRuleById(request.RuleId, cancellationToken);

        if (rule == null)
        {
            response.Message = "Rule not found";
            return response;
        }

        var reSettings = new ReSettings
        {
            CustomTypes = new Type[] { typeof(Common) },

        };

        var DocumentalRule = await _docRulesRepository.GetDynamicFormByKey(rule.KeyDocument);
        var responseExecute = await _processRuleEngine.Execute(DocumentalRule.workflowRules, rule, request.ParamInput, reSettings, cancellationToken);
        response.ParamOutPut = responseExecute.ParamOutPut;
        response.Message = responseExecute.Message;






        return response;
    }
}
