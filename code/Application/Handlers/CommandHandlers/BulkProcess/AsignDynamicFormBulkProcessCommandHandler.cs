using Application.Interfaces.Repositories;
using Application.RequestModels.CommandRequestModels.BulkProcess;
using Application.ResponseModels.CommandResponseModels.BulkProcess;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application;


public class AsignDynamicFormBulkProcessCommandHandler : IRequestHandler<AsignDynamicFormBulkProcessCommandRequest, AsignDynamicFormBulkProcessCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly ILogger<AsignDynamicFormBulkProcessCommandHandler> _logger;
    private readonly IBulkRepository _repository;

    public AsignDynamicFormBulkProcessCommandHandler(IMapper mapper, IBulkRepository repository, ILogger<AsignDynamicFormBulkProcessCommandHandler> logger)
    {
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
    }

    public async Task<AsignDynamicFormBulkProcessCommandResponse> Handle(AsignDynamicFormBulkProcessCommandRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = new AsignDynamicFormBulkProcessCommandResponse();

            await _repository.AsignDynamicForms(request.BulkProcessId, request.DynamicFormsListId, cancellationToken);


            return await Task.Run(() => response);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.StackTrace);
            throw;
        }
    }
}
