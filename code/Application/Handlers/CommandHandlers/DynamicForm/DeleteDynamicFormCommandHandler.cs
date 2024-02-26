using Application.Interfaces.Repositories;
using Application.RequestModels.CommandRequestModels;
using Application.ResponseModels.CommandResponseModels;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.CommandHandlers.DynamicForm
{
    public class DeleteDynamicFormCommandHandler : IRequestHandler<DeleteDynamicFormCommandRequest, DeleteDynamicFormCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteDynamicFormCommandHandler> _logger;
        private readonly IDynamicFormRepository _repository;

        public DeleteDynamicFormCommandHandler(IMapper mapper, IDynamicFormRepository repository, ILogger<DeleteDynamicFormCommandHandler> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public async Task<DeleteDynamicFormCommandResponse> Handle(DeleteDynamicFormCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var workflow = await _repository.GetByIdAsync(request.Id);
                if (workflow == null)
                    return null;

                var response = new DeleteDynamicFormCommandResponse();

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
