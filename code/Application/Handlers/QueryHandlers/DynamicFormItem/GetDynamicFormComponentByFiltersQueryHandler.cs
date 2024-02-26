using Application.Dto.Params.DynamicFormItem;
using Application.Interfaces.Repositories;
using Application.RequestModels.Extensions;
using Application.RequestModels.QueriesRequestModels.DynamicFormItem;
using Application.ResponseModels.QueriesResponseModels.DynamicFormItem;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.QueryHandlers.DynamicFormItem;

public class GetDynamicFormComponentByFiltersQueryHandler : IRequestHandler<GetDynamicFormComponentByFiltersQueryRequest, GetDynamicFormComponentByFiltersQueryResponse>
{
    private readonly IDynamicFormComponentRuleRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetDynamicFormComponentByFiltersQueryHandler> _logger;
    public GetDynamicFormComponentByFiltersQueryHandler(IDynamicFormComponentRuleRepository repository, IMapper mapper, ILogger<GetDynamicFormComponentByFiltersQueryHandler> logger
   )
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<GetDynamicFormComponentByFiltersQueryResponse> Handle(GetDynamicFormComponentByFiltersQueryRequest request, CancellationToken cancellationToken)
    {

        try
        {
            var response = new GetDynamicFormComponentByFiltersQueryResponse();

            var components = await _repository.GetFilteredAsync(request.Filter, cancellationToken);
            var componentsDto = _mapper.Map<List<DynamicFormComponentRuleDto>>(components);

            response.Component = PaginatedList<DynamicFormComponentRuleDto>.Create(componentsDto, request.Filter.PageIndex, request.Filter.PageSize, string.Empty, string.Empty);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.StackTrace);
            throw;
        }
    }
}
