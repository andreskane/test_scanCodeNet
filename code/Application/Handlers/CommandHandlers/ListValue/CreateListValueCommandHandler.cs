using Application.Dto;
using Application.Interfaces.Repositories;
using Application.RequestModels.CommandRequestModels.ListValue;
using Application.ResponseModels.CommandResponseModels.ListValue;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.CommandHandlers.ListValue
{
    public class CreateListValueCommandHandler : IRequestHandler<CreateListValueCommandRequest, CreateListValueCommandResponse>
    {
        private readonly ILogger<CreateListValueCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IListValueRepository _repositoryAsync;

        public CreateListValueCommandHandler(ILogger<CreateListValueCommandHandler> logger,
                                             IMapper mapper,
                                             IListValueRepository genericRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _repositoryAsync = genericRepository;
        }

        public async Task<CreateListValueCommandResponse> Handle(CreateListValueCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {

                var response = new CreateListValueCommandResponse();

                var listValues = _mapper.Map<List<Domain.Entities.ListAggregate.ListValue>>(request.ListValues);

                await _repositoryAsync.AddRangeAsync(listValues);

                response.ListValues = _mapper.Map<List<ListValueDto>>(listValues);

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
