using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.CommandHandlers.ListValue
{
    public class DeleleListValueCommandHandler : IRequestHandler<DeleteListValueCommandRequest, DeleteListValueCommandResponse>
    {
        private readonly ILogger<DeleleListValueCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IListValueRepository _repositoryAsync;

        public DeleleListValueCommandHandler(ILogger<DeleleListValueCommandHandler> logger,
                                             IMapper mapper,
                                             IListValueRepository genericRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _repositoryAsync = genericRepository;
        }


        public async Task<DeleteListValueCommandResponse> Handle(DeleteListValueCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new DeleteListValueCommandResponse();

                var listValue = await _repositoryAsync.GetByIdAsync(request.ListValueId);

                if (listValue != null)
                {
                    response.Deleted = await _repositoryAsync.DeleteAsync(listValue, cancellationToken);
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                throw ex;
            }
        }

    }

    public class DeleteListValueCommandResponse
    {
        public int Deleted { get; set; }
    }

    public class DeleteListValueCommandRequest : IRequest<DeleteListValueCommandResponse>
    {
        public Int64 ListValueId { get; set; }
    }
}


