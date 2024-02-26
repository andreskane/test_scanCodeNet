using Application.Dto;
using Application.Interfaces.Repositories;
using Application.RequestModels.CommandRequestModels.BulkProcess;
using Application.ResponseModels.CommandResponseModels.BulkProcess;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.QueryHandlers.BulkProcess
{
    public class GetAllComponentsQueryHandler : IRequestHandler<GetAllComponentQueryRequest, GetAllComponentsQueryResponse>
    {
        private readonly IBulkRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllComponentsQueryHandler> _logger;
        public GetAllComponentsQueryHandler(
                IBulkRepository repo,
                IMapper mapper,
                ILogger<GetAllComponentsQueryHandler> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<GetAllComponentsQueryResponse> Handle(GetAllComponentQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new GetAllComponentsQueryResponse();

                var components = await _repo.GetBulkComponentsByBulkProcessId(request.BulkProcessId, cancellationToken);
                response.ComponentList = _mapper.Map<List<BulckComponentDto>>(components);

                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
