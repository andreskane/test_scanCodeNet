using Application.Dto;
using Application.Interfaces;
using Application.RequestModels.CommandRequestModels;
using Application.ResponseModels.CommandResponseModels;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.CommandHandlers.WorkflowProductAttributes
{

    public class CreateDynamicFormProductAttributesCommandHandler : IRequestHandler<CreateDynamicFormProductAttributesCommandRequest, CreateDynamicFormProductAttributesCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateDynamicFormProductAttributesCommandHandler> _logger;
        private readonly IGenericRepositoryAsync<Domain.Entities.DynamicFormAggregate.DynamicFormProductAttributes> _repository;


        public CreateDynamicFormProductAttributesCommandHandler(IMapper mapper, IGenericRepositoryAsync<Domain.Entities.DynamicFormAggregate.DynamicFormProductAttributes> repository, ILogger<CreateDynamicFormProductAttributesCommandHandler> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public async Task<CreateDynamicFormProductAttributesCommandResponse> Handle(CreateDynamicFormProductAttributesCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new CreateDynamicFormProductAttributesCommandResponse();

                var workflow = _mapper.Map<Domain.Entities.DynamicFormAggregate.DynamicFormProductAttributes>(request.WProductAttribute);
                await _repository.AddAsync(workflow);

                response.WProductAttribute = _mapper.Map<DynamicFormProductAttributeDto>(workflow);
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
