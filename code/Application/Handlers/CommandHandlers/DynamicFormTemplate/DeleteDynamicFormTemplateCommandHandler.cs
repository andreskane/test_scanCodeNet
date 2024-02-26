using Application.Interfaces.Repositories;
using Application.RequestModels.CommandRequestModels;
using Application.ResponseModels.CommandResponseModels;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.CommandHandlers.WorkflowTemplate
{
    public class DeleteDynamicFormTemplateCommandHandler : IRequestHandler<DeleteDynamicFormTemplateCommandRequest, DeleteDynamicFormTemplateCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteDynamicFormTemplateCommandHandler> _logger;
        private readonly IGenericRepositoryAsync<Domain.Entities.DynamicFormAggregate.DynamicFormTemplate> _repository;


        public DeleteDynamicFormTemplateCommandHandler(IMapper mapper, IGenericRepositoryAsync<Domain.Entities.DynamicFormAggregate.DynamicFormTemplate> repository, ILogger<DeleteDynamicFormTemplateCommandHandler> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public async Task<DeleteDynamicFormTemplateCommandResponse> Handle(DeleteDynamicFormTemplateCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var workflow = await _repository.GetByIdAsync(request.Id);
                if (workflow == null)
                    return null;

                var response = new DeleteDynamicFormTemplateCommandResponse();

                var deleted = await _repository.DeleteAsync(workflow, cancellationToken);
                response.deleted = deleted > 0;

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
