using Application.Dto;
using Application.Interfaces.Repositories;
using Application.RequestModels.QueriesRequestModels.ListValue;
using Application.ResponseModels.QueriesResponseModels.ListValue;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.QueryHandlers.ListValue
{
    public class GetListRowQueryHandler : IRequestHandler<GetListRowQueryRequest, GetListRowQueryResponse>
    {
        private readonly ILogger<GetListRowQueryHandler> _logger;
        private readonly IListValueRepository _repository;
        private readonly IMapper _mapper;

        public GetListRowQueryHandler(ILogger<GetListRowQueryHandler> logger,
                                      IListValueRepository repository,
                                      IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetListRowQueryResponse> Handle(GetListRowQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new GetListRowQueryResponse();

                var pagedList = await _repository.GetFilteredAsync(request.Filter, cancellationToken);

                var listRowDto = _mapper.Map<List<ListValueDto>>(pagedList);

                response.ListRows = listRowDto;

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
