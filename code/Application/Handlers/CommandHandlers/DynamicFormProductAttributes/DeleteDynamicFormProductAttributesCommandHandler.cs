using Application.Interfaces;
using Application.RequestModels.CommandRequestModels;
using Application.ResponseModels.CommandResponseModel;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.CommandHandlers.WorkflowProductAttributes
{

    public class DeleteDynamicFormProductAttributesCommandHandler : IRequestHandler<DeleteDynamicFormProductAttributesCommandRequest, DeleteDynamicFormProductAttributesCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteDynamicFormProductAttributesCommandHandler> _logger;
        private readonly IGenericRepositoryAsync<Domain.Entities.DynamicFormAggregate.DynamicFormProductAttributes> _repository;

        public DeleteDynamicFormProductAttributesCommandHandler(IMapper mapper, IGenericRepositoryAsync<Domain.Entities.DynamicFormAggregate.DynamicFormProductAttributes> repository, ILogger<DeleteDynamicFormProductAttributesCommandHandler> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public async Task<DeleteDynamicFormProductAttributesCommandResponse> Handle(DeleteDynamicFormProductAttributesCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var productAttribute = await _repository.GetByIdAsync(request.Id);
                if (productAttribute == null)
                    return null;

                var response = new DeleteDynamicFormProductAttributesCommandResponse();

                var deleted = await _repository.DeleteAsync(productAttribute, cancellationToken);
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
