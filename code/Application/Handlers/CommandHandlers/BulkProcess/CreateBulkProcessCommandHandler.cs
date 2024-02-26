using Application.Dto;
using Application.Interfaces.Repositories;
using Application.RequestModels.CommandRequestModels.BulkProcess;
using Application.ResponseModels.CommandResponseModels.BulkProcess;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.CommandHandlers;

public class CreateBulkProcessCommandHandler : IRequestHandler<CreateBulkProcessCommandRequest, CreateBulkProcessCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly ILogger<CreateBulkProcessCommandHandler> _logger;
    private readonly IBulkRepository _repository;

    public CreateBulkProcessCommandHandler(IMapper mapper, IBulkRepository repository, ILogger<CreateBulkProcessCommandHandler> logger)
    {
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
    }

    public async Task<CreateBulkProcessCommandResponse> Handle(CreateBulkProcessCommandRequest request, CancellationToken cancellationToken)
    {
        try
        {


            BulkProcess newbulkProcess = new BulkProcess();


            newbulkProcess.Name = request.Name;

            newbulkProcess.ProcessType = request.ProcessType;
            newbulkProcess.Status = request.Status;





            var res = await _repository.AddAsync(newbulkProcess);

            var response = new CreateBulkProcessCommandResponse();
            response.BulkProcess = _mapper.Map<BulkProcessDto>(res);
            return await Task.Run(() => response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.StackTrace);
            throw;
        }
    }
}
