using Application.Dto;
using Application.Dto.Params;
using Application.Interfaces.Repositories;
using Application.RequestModels.Extensions;
using Application.RequestModels.QueriesRequestModels.Components;
using Application.ResponseModels.QueriesResponseModels.Components;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.QueryHandlers.Components
{
    public class GEtTemplateComponentsQueryHandler : IRequestHandler<GetTemplateComponentsQueryRequest, GetTemplateComponentsQueryResponse>
    {

        private readonly ITemplateComponentRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<GEtTemplateComponentsQueryHandler> _logger;
        public GEtTemplateComponentsQueryHandler(
     ITemplateComponentRepository repo,
     IMapper mapper,
       ILogger<GEtTemplateComponentsQueryHandler> logger
       )
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<GetTemplateComponentsQueryResponse> Handle(GetTemplateComponentsQueryRequest request, CancellationToken cancellationToken)
        {

            try
            {
                var response = new GetTemplateComponentsQueryResponse();
                var filter = _mapper.Map<FilterTemplateComponentsDto>(request.Filter);

                var components = await _repo.GetFilteredAsync(filter, cancellationToken);
                var componentsDto = _mapper.Map<List<TemplateComponentDto>>(components);

                var pagedList = PaginatedList<TemplateComponentDto>.Create(componentsDto, request.Filter.PageIndex, request.Filter.PageSize, string.Empty, string.Empty);
                response.TemplateComponents = pagedList;

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
