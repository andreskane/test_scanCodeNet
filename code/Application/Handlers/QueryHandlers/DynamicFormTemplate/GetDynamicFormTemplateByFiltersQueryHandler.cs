using Application.Dto;
using Application.Dto.Params.DynamicForm;
using Application.Interfaces.Repositories;
using Application.RequestModels.Extensions;
using Application.RequestModels.QueriesRequestModels;
using Application.ResponseModels.QueriesResponseModels;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.QueryHandlers
{
    public class GetDynamicFormTemplateByFiltersQueryHandler : IRequestHandler<GetDynamicFormTemplateByFiltersQueryRequest, GetDynamicFormTemplateByFiltersQueryResponse>
    {
        private readonly IDynamicFormTemplateRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDynamicFormTemplateByFiltersQueryHandler> _logger;
        public GetDynamicFormTemplateByFiltersQueryHandler(IDynamicFormTemplateRepository repository, IMapper mapper, ILogger<GetDynamicFormTemplateByFiltersQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetDynamicFormTemplateByFiltersQueryResponse> Handle(GetDynamicFormTemplateByFiltersQueryRequest request, CancellationToken cancellationToken)
        {

            try
            {
                var response = new GetDynamicFormTemplateByFiltersQueryResponse();
                var filter = _mapper.Map<FilterDynamicForm>(request.Filter);

                var pagedList = await _repository.GetFilteredAsync(filter, cancellationToken);
                var oList = _mapper.Map<List<DynamicFormTemplateDto>>(pagedList);

                response.WorkflowsTemplates = PaginatedList<DynamicFormTemplateDto>.Create(oList.ToList(), request.Filter.PageIndex, request.Filter.PageSize, string.Empty, string.Empty);



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
