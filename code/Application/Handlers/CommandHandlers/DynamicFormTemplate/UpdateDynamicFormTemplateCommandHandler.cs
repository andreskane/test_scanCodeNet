using Application.Interfaces.Repositories;
using Application.RequestModels.CommandRequestModels;
using Application.ResponseModels.CommandResponseModels;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.CommandHandlers.WorkflowTemplate
{
    public class UpdateDynamicFormTemplateCommandHandler : IRequestHandler<UpdateDynamicFormTemplateCommandRequest, UpdateDynamicFormTemplateCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateDynamicFormTemplateCommandHandler> _logger;
        private readonly IGenericRepositoryAsync<Domain.Entities.DynamicFormAggregate.DynamicFormTemplate> _repository;


        public UpdateDynamicFormTemplateCommandHandler(IMapper mapper, IGenericRepositoryAsync<Domain.Entities.DynamicFormAggregate.DynamicFormTemplate> repository, ILogger<UpdateDynamicFormTemplateCommandHandler> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public async Task<UpdateDynamicFormTemplateCommandResponse> Handle(UpdateDynamicFormTemplateCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var workflow = await _repository.GetByIdAsync(request.DynamicFormTemplate.Id);
                if (workflow == null)
                    return null;

                var response = new UpdateDynamicFormTemplateCommandResponse();
                _mapper.Map(request.DynamicFormTemplate, workflow);
                workflow.Id = request.DynamicFormTemplate.Id;

                await _repository.UpdateAsync(workflow, cancellationToken);
                response.DynamicFormTemplate = request.DynamicFormTemplate;
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
