using Application.Interfaces.Repositories;
using Application.RequestModels.CommandRequestModels.Rule;
using Application.ResponseModels.CommandResponseModels.Rules;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.CommandHandlers.Rule
{
    public class UpdateEnabledRuleCommandHandler : IRequestHandler<UpdateEnabledRuleCommandRequest, UpdateEnabledRuleCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateEnabledRuleCommandHandler> _logger;
        private readonly IGenericRepositoryAsync<Domain.Entities.RulesAggregate.RuleDynamic> _ruleRepository;

        public UpdateEnabledRuleCommandHandler(IMapper mapper,
            IGenericRepositoryAsync<Domain.Entities.RulesAggregate.RuleDynamic> ruleRepository,
            ILogger<UpdateEnabledRuleCommandHandler> logger)
        {
            _mapper = mapper;
            _ruleRepository = ruleRepository;
            _logger = logger;
        }

        public async Task<UpdateEnabledRuleCommandResponse> Handle(UpdateEnabledRuleCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var rule = await _ruleRepository.GetByIdAsync(request.RuleId);
                if (rule == null)
                    return null;

                var response = new UpdateEnabledRuleCommandResponse();

                rule.Enabled = request.Enabled;

                await _ruleRepository.UpdateAsync(rule, cancellationToken);
                response.Enabled = rule.Enabled;

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
