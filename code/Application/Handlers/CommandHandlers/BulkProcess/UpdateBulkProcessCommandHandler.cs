using Application.Dto;
using Application.Interfaces.Repositories;
using Application.RequestModels.CommandRequestModels.BulkProcess;
using Application.ResponseModels.CommandResponseModels.BulkProcess;
using AutoMapper;
using ConnectureOS.Framework.Net.RestClient;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application;



public class UpdateBulkProcessCommandHandler : IRequestHandler<UpdateBulkProcessCommandRequest, UpdateBulkProcessCommandResponse>
{
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateBulkProcessCommandHandler> _logger;
    private readonly IBulkRepository _repository;
    private readonly IBulckComponentRepository _bulckComponentRepository;
    public UpdateBulkProcessCommandHandler(IMapper mapper, IBulkRepository repository,
        IBulckComponentRepository bulckComponentRepository,
        ILogger<UpdateBulkProcessCommandHandler> logger)
    {
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
        _bulckComponentRepository = bulckComponentRepository;
    }

    public async Task<UpdateBulkProcessCommandResponse> Handle(UpdateBulkProcessCommandRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = new UpdateBulkProcessCommandResponse();

            if (request.bulkProcess.id == null)
                throw new BadRequestException("BulkProcess Id is required");


            var bulckprocess = await _repository.GetByIdAsync((long)request.bulkProcess.id);

            if (bulckprocess == null)
                throw new BadRequestException("BulkProcess not found");

            bulckprocess.Name = request.bulkProcess.Name;
            bulckprocess.Status = request.bulkProcess.Status;
            bulckprocess.StartDate = DateTime.Now;
            bulckprocess.ProcessType = request.bulkProcess.ProcessType;
            bulckprocess.PlacementPreference = request.bulkProcess.PlacementPreference;
            bulckprocess.ComponentOfReference = request.bulkProcess.ComponentOfReference;
 
            var res = await _repository.UpdateAsync(bulckprocess, default);

            var bulckComponentParam = _mapper.Map<List<Domain.Entities.BulckComponent>>(request.bulkProcess.ComponentListItems).ToList();

            var bulckComponents = await _bulckComponentRepository.GetBulkComponentByBulckId(bulckprocess.Id, cancellationToken);

            foreach (var item in bulckComponents)
            {
                await _bulckComponentRepository.DeleteAsync(item, cancellationToken);
            }
 
            bulckComponentParam.ForEach(x => x.BulkProcess = bulckprocess);

            await _bulckComponentRepository.AddRangeAsync(bulckComponentParam.ToList(), cancellationToken);

            bulckprocess.ComponentListItems = bulckComponentParam;
 

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
