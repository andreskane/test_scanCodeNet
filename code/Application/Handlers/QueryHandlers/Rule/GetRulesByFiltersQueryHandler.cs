using Application.Dto;
using Application.Dto.Params;
using Application.Interfaces.Repositories;
using Application.RequestModels.Extensions;
using Application.RequestModels.QueriesRequestModels.Rule;
using Application.ResponseModels.QueriesResponseModels.Rule;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.QueryHandlers.Rule
{
    public class GetRulesByFiltersQueryHandler : IRequestHandler<GetRulesByFilterQueryRequest, GetRulesByFilterQueryResponse>
    {
        private readonly IRuleRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetRulesByFiltersQueryHandler> _logger;
        public GetRulesByFiltersQueryHandler(IRuleRepository repository, IMapper mapper, ILogger<GetRulesByFiltersQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetRulesByFilterQueryResponse> Handle(GetRulesByFilterQueryRequest request, CancellationToken cancellationToken)
        {
            var response = new GetRulesByFilterQueryResponse();

            var filter = _mapper.Map<PaginatedRequestDto>(request.Filter);

            var pagedList = await _repository.GetFilteredAsync(filter, cancellationToken);

            var oList = new List<RuleDto>();
            foreach (var rule in pagedList)
            {
                var ruleDto = _mapper.Map<RuleDto>(rule);
                ruleDto.DinamicFormsCount = rule.DynamicFormRules.Count;
                oList.Add(ruleDto);
            };

            response.Rules = PaginatedList<RuleDto>.Create(oList.ToList(), request.Filter.PageIndex, request.Filter.PageSize, string.Empty, string.Empty);

            return response;
        }
    }
}
