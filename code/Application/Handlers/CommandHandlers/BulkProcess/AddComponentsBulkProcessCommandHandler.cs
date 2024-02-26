using Application.Interfaces.Repositories;
using Application.RequestModels.CommandRequestModels.BulkProcess;
using Application.ResponseModels.CommandResponseModels.BulkProcess;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application;


public class AddComponentsBulkProcessCommandHandler : IRequestHandler<AddComponentsBulkProcessCommandRequest, AddComponentsBulkProcessCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly ILogger<AddComponentsBulkProcessCommandHandler> _logger;
    private readonly IBulkRepository _repository;

    public AddComponentsBulkProcessCommandHandler(IMapper mapper, IBulkRepository repository, ILogger<AddComponentsBulkProcessCommandHandler> logger)
    {
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
    }

    public async Task<AddComponentsBulkProcessCommandResponse> Handle(AddComponentsBulkProcessCommandRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = new AddComponentsBulkProcessCommandResponse();
 
            return await Task.Run(() => response);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.StackTrace);
            throw;
        }
    }
}
