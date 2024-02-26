using Application.Interfaces.Repositories;
using Application.RequestModels.CommandRequestModels.Rule;
using Application.ResponseModels.CommandResponseModels.Rules;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.CommandHandlers.Rule
{
    public class DeleteRuleCommandHandler : IRequestHandler<DeleteRuleCommandRequest, DeleteRuleCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteRuleCommandHandler> _logger;
        private readonly IGenericRepositoryAsync<Domain.Entities.RulesAggregate.RuleDynamic> _ruleRepository;

        public DeleteRuleCommandHandler(IMapper mapper,
            IGenericRepositoryAsync<Domain.Entities.RulesAggregate.RuleDynamic> ruleRepository,
            ILogger<DeleteRuleCommandHandler> logger)
        {
            _mapper = mapper;
            _ruleRepository = ruleRepository;
            _logger = logger;
        }

        public async Task<DeleteRuleCommandResponse> Handle(DeleteRuleCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var rule = await _ruleRepository.GetByIdAsync(request.Id);
                if (rule == null)
                    return null;

                var response = new DeleteRuleCommandResponse();

                var deleted = await _ruleRepository.DeleteAsync(rule, cancellationToken);
                response.deleted = deleted > 0;

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
