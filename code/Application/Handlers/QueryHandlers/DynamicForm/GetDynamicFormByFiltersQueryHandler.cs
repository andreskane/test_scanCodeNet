using Application.Dto;
using Application.Dto.Params.DynamicForm;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.RequestModels.Extensions;
using Application.RequestModels.QueriesRequestModels;
using Application.ResponseModels.QueriesResponseModels;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.QueryHandlers
{
    public class GetDynamicFormByFiltersQueryHandler : IRequestHandler<GetDynamicFormByFiltersQueryRequest, GetDynamicFormByFiltersQueryResponse>
    {
        private readonly IDynamicFormRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDynamicFormByFiltersQueryHandler> _logger;
        private readonly ICurrentUserService _currentUserService;
        public GetDynamicFormByFiltersQueryHandler(
            IDynamicFormRepository repository,
            IMapper mapper,
            ILogger<GetDynamicFormByFiltersQueryHandler> logger,
            ICurrentUserService currentUserService
       )
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<GetDynamicFormByFiltersQueryResponse> Handle(GetDynamicFormByFiltersQueryRequest request, CancellationToken cancellationToken)
        {

            try
            {
                //var auxiliar=_currentUserService.tenantId;
                var response = new GetDynamicFormByFiltersQueryResponse();
                var filter = _mapper.Map<FilterDynamicForm>(request.Filter);

                var pagedList = await _repository.GetFilteredAsync(filter, cancellationToken);
                var oList = _mapper.Map<List<DynamicFormDto>>(pagedList);

                response.Workflows = PaginatedList<DynamicFormDto>.Create(oList.ToList(), request.Filter.PageIndex, request.Filter.PageSize, string.Empty, string.Empty);



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
