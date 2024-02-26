using Application.Dto;
using Application.Helper;
using Application.Interfaces.Documental;
using Application.Interfaces.Repositories;
using Application.RequestModels.CommandRequestModels.Rule;
using Application.ResponseModels.CommandResponseModels.Rules;
using AutoMapper;
using ConnectureOS.Framework.Net.RestClient;
using Domain.Entities.RulesAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using RulesEngine.Models;

namespace Application.Handlers.CommandHandlers.Rule
{
    public class UpdateRuleCommandHandler : IRequestHandler<UpdateRuleCommandRequest, UpdateRuleCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateRuleCommandHandler> _logger;
        private readonly IGenericRepositoryAsync<Domain.Entities.RulesAggregate.RuleDynamic> _ruleRepository;
        private readonly IRuleRepository _ruleRepositoryA;

        private readonly IRuleActionRepository _ruleActionRepository;
        private readonly IActionParameterRepository _actionParameterRepository;
        private readonly IDocRulesRepository _docRulesRepository;

        public UpdateRuleCommandHandler(IMapper mapper,
            IGenericRepositoryAsync<Domain.Entities.RulesAggregate.RuleDynamic> ruleRepository,
            IRuleActionRepository ruleActionRepository,
            IActionParameterRepository actionParameterRepository,
            IDocRulesRepository docRulesRepository,
            IRuleRepository ruleRepositoryA,
            ILogger<UpdateRuleCommandHandler> logger)
        {
            _mapper = mapper;
            _ruleRepository = ruleRepository;
            _ruleActionRepository = ruleActionRepository;
            _actionParameterRepository = actionParameterRepository;
            _logger = logger;
            _docRulesRepository = docRulesRepository;
            _ruleRepositoryA = ruleRepositoryA;
        }

        public async Task<UpdateRuleCommandResponse> Handle(UpdateRuleCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new UpdateRuleCommandResponse();

                if (request.Rule.Id != null)
                {
                    var rule = await _ruleRepository.GetByIdAsync((long)request.Rule.Id);

                    if (rule == null)
                        throw new BadRequestException("Rule not found");


                    rule.Name = request.Rule.Name;
                    rule.Enabled = request.Rule.Enabled;
                    rule.Description = request.Rule.Description;
                    rule.Version = request.Rule.Version;

                    await _ruleRepository.UpdateAsync(rule, cancellationToken);

                    var ruleActionsParam = _mapper.Map<List<Domain.Entities.RulesAggregate.RuleAction>>(request.Rule.Actions).ToList();

                    var ruleActions = await _ruleActionRepository.GetRuleActionsByRuleId(rule.Id, cancellationToken);


                    foreach (var value in ruleActions)
                        await _ruleActionRepository.RealDeleteAsync(value, cancellationToken);


                    await AddRuleAction(ruleActionsParam, cancellationToken);

                    rule.Actions = ruleActionsParam;


                    //new doc rules

                    var RootRule = new RootRules();
                    RootRule.workflowRules = new List<Workflow>();
                    RootRule.AddWorkflowRule(rule);


                    if (rule.KeyDocument != null)
                    {
                        await _docRulesRepository.UpdateDynamicForm(RootRule, rule.KeyDocument);

                    }
                    else
                    {
                        var key = await _docRulesRepository.InsertDynamicForm(RootRule);

                        rule.KeyDocument = key;
                    }

                    response.Rule = _mapper.Map<RuleDto>(rule);
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                throw ex;
            }

        }

        private async Task AddRuleAction(IList<Domain.Entities.RulesAggregate.RuleAction> rulesActionsToAdd, CancellationToken cancellationToken)
        {
            foreach (var item in rulesActionsToAdd)
            {
                var oAcction = await _ruleActionRepository.AddAsync(new Domain.Entities.RulesAggregate.RuleAction()
                {
                    DeletedBy = item.DeletedBy,
                    Deleted = item.Deleted,
                    IsDeleted = item.IsDeleted,
                    Created = item.Created,
                    CreatedBy = item.CreatedBy,
                    LastModified = item.LastModified,
                    LastModifiedBy = item.LastModifiedBy,
                    ActionType = item.ActionType,
                    Description = item.Description,
                    Name = item.Name,
                    Order = item.Order,
                    RequestBy = item.RequestBy,
                    RequestType = item.RequestType,
                    RuleId = item.RuleId,
                    WhenScript = item.WhenScript,
                    ThenScript = item.ThenScript,
                    Url = item.Url,
                    Version = item.Version,
                    IsActive = item.IsActive


                }, cancellationToken);


                foreach (var para in item.Parameters)
                {

                    await _actionParameterRepository.AddAsync(new ActionParameter()
                    {

                        Name = para.Name,
                        DataType = para.DataType,
                        IsRequest = para.IsRequest,
                        RuleActtioId = oAcction.Id,


                    }, cancellationToken);
                }


            }


        }

        private async Task UpdateRuleActions(IList<Domain.Entities.RulesAggregate.RuleAction> rulesActionsToUpdate, IList<Domain.Entities.RulesAggregate.RuleAction> ruleActions, CancellationToken cancellationToken)
        {

            foreach (var item in rulesActionsToUpdate)
            {
                var entityToUpdate = ruleActions.Where(x => x.Id == item.Id).FirstOrDefault();

                item.ActionType = entityToUpdate.ActionType;
                item.RequestType = entityToUpdate.RequestType;
                item.RequestBy = entityToUpdate.RequestBy;

                item.Name = entityToUpdate.Name;
                item.Description = entityToUpdate.Description;
                item.Order = entityToUpdate.Order;
                item.Version = entityToUpdate.Version;
                item.WhenScript = entityToUpdate.WhenScript;
                item.ThenScript = entityToUpdate.ThenScript;
                item.Url = entityToUpdate.Url;
                item.IsActive = entityToUpdate.IsActive;

                await _ruleActionRepository.UpdateAsync(item, cancellationToken);


                //find elements to update
                var actionParamP = _mapper.Map<List<ActionParameter>>(item.Parameters).ToList();

                var actionParam = await _actionParameterRepository.GetActionParameterByRuleActionId(item.Id, cancellationToken);

                //Delete missing elemnts
                var actionParamToDelete = actionParam.Except(actionParamP, new ActionParameterComparer()).ToList();

                foreach (var value in actionParamToDelete)
                    await _actionParameterRepository.DeleteAsync(value, cancellationToken);

                //Add new elements
                var actionParamToAdd = actionParamP.Except(actionParam, new ActionParameterComparer()).ToList();

                foreach (var aParam in actionParamToAdd)
                {
                    aParam.RuleActtioId = item.Id;

                    await _actionParameterRepository.AddAsync(aParam, cancellationToken);
                }

                item.Parameters = actionParam;
            }
        }

    }
}
