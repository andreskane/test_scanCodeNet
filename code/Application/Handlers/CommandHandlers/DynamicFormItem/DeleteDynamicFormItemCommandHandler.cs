using Application.Interfaces.Repositories;
using Application.RequestModels.CommandRequestModels;
using Application.ResponseModels.CommandResponseModels;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.CommandHandlers.DynamicFormItem
{

    public class DeleteDynamicFormItemCommandHandler : IRequestHandler<DeleteDynamicFormItemCommandRequest, DeleteDynamicFormItemCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteDynamicFormItemCommandHandler> _logger;
        private readonly IGenericRepositoryAsync<Domain.Entities.DynamicFormAggregate.DynamicFormItem> _repository;

        public DeleteDynamicFormItemCommandHandler(IMapper mapper, IGenericRepositoryAsync<Domain.Entities.DynamicFormAggregate.DynamicFormItem> repository, ILogger<DeleteDynamicFormItemCommandHandler> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public async Task<DeleteDynamicFormItemCommandResponse> Handle(DeleteDynamicFormItemCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var dynamicForm = await _repository.GetByIdAsync(request.Id);
                if (dynamicForm == null)
                    return null;

                var response = new DeleteDynamicFormItemCommandResponse();

                var deleted = await _repository.DeleteAsync(dynamicForm, cancellationToken);
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
