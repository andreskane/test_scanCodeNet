using Application.Dto;
using Application.Interfaces.Repositories;
using Application.RequestModels.QueriesRequestModels.Rule;
using Application.ResponseModels.QueriesResponseModels.Rule;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.QueryHandlers.Rule
{
    public class GetRuleByIdQueryHandler : IRequestHandler<GetRuleByIdQueryRequest, GetRuleByIdQueryResponse>
    {
        private readonly IRuleRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetRuleByIdQueryHandler> _logger;
        public GetRuleByIdQueryHandler(IRuleRepository repository, IMapper mapper, ILogger<GetRuleByIdQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetRuleByIdQueryResponse> Handle(GetRuleByIdQueryRequest request, CancellationToken cancellationToken)
        {

            try
            {
                var response = new GetRuleByIdQueryResponse();

                var rule = await _repository.GetRuleById(request.Id, cancellationToken);

                var actionsDto = new List<RuleActionDto>();
                foreach (var action in rule.Actions)
                {
                    actionsDto.Add(_mapper.Map<RuleActionDto>(action));
                }

                var ruleDto = _mapper.Map<RuleDto>(rule);
                ruleDto.Actions = actionsDto;

                response.Rule = ruleDto;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                throw;
            }
        }
    }
}
