using ApiOS.Controllers;
using Application.Dto;
using Application.Interfaces.Repositories;
using Application.RequestModels.Extensions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.QueryHandlers.BulkProcess
{
    public class GetAllBulkProcessQueryHandler : IRequestHandler<GetAllBulkProcessQueryRequest, GetAllBulkProcessQueryResponse>
    {
        private readonly IBulkRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllBulkProcessQueryHandler> _logger;
        public GetAllBulkProcessQueryHandler(
     IBulkRepository repo,
     IMapper mapper,
       ILogger<GetAllBulkProcessQueryHandler> logger
       )
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<GetAllBulkProcessQueryResponse> Handle(GetAllBulkProcessQueryRequest request, CancellationToken cancellationToken)
        {

            try
            {
                var response = new GetAllBulkProcessQueryResponse();

                var components = await _repo.GetBulkProcessesAsyncExcludeDraft(cancellationToken);
                if (components == null)
                {
                    return response;
                }
                var componentsDto = _mapper.Map<List<BulkProcessDto>>(components);

                var pagedList = PaginatedList<BulkProcessDto>.Create(componentsDto, request.Filter.PageIndex, request.Filter.PageSize, string.Empty, string.Empty);
                response.bulkProcessList = pagedList;

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
