using Application.Interfaces;
using Application.RequestModels.CommandRequestModels;
using Application.ResponseModels.CommandResponseModels;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.CommandHandlers.WorkflowProductAttributes
{
    public class UpdateDynamicFormProductAttributesCommandHandler : IRequestHandler<UpdateDynamicFormProductAttributesCommandRequest, UpdateDynamicFormProductAttributesCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateDynamicFormProductAttributesCommandHandler> _logger;
        private readonly IGenericRepositoryAsync<Domain.Entities.DynamicFormAggregate.DynamicFormProductAttributes> _repository;


        public UpdateDynamicFormProductAttributesCommandHandler(IMapper mapper, IGenericRepositoryAsync<Domain.Entities.DynamicFormAggregate.DynamicFormProductAttributes> repository, ILogger<UpdateDynamicFormProductAttributesCommandHandler> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public async Task<UpdateDynamicFormProductAttributesCommandResponse> Handle(UpdateDynamicFormProductAttributesCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var productAttribute = await _repository.GetByIdAsync(request.DynamicFormProductAttribute.Id);
                if (productAttribute == null)
                    return null;

                var response = new UpdateDynamicFormProductAttributesCommandResponse();
                _mapper.Map(request.DynamicFormProductAttribute, productAttribute);
                productAttribute.Id = request.DynamicFormProductAttribute.Id;

                await _repository.UpdateAsync(productAttribute, cancellationToken);
                response.WProductAttribute = request.DynamicFormProductAttribute;
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
