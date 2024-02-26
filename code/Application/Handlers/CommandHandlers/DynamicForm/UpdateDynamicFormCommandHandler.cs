using Application.Interfaces.Repositories;
using Application.RequestModels.CommandRequestModels;
using Application.ResponseModels.CommandResponseModels;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.CommandHandlers.DynamicForm
{
    public class UpdateDynamicFormCommandHandler : IRequestHandler<UpdateDynamicFormCommandRequest, UpdateDynamicFormCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateDynamicFormCommandHandler> _logger;
        private readonly IDynamicFormRepository _repository;


        public UpdateDynamicFormCommandHandler(IMapper mapper, IDynamicFormRepository repository, ILogger<UpdateDynamicFormCommandHandler> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }
   
        public async Task<UpdateDynamicFormCommandResponse> Handle(UpdateDynamicFormCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var workflow = await _repository.GetByIdAsync(request.Workflow.Id);
                if (workflow == null)
                    return null;

                UpdateDynamicFormCommandResponse response = new UpdateDynamicFormCommandResponse();
                _mapper.Map(request.Workflow, workflow);
                workflow.Id = request.Workflow.Id;

                await _repository.UpdateAsync(workflow, cancellationToken);
                response.Workflow = request.Workflow;
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
