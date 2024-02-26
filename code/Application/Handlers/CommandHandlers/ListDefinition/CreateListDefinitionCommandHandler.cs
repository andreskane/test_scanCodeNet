using Application.Dto;
using Application.Interfaces.Repositories;
using Application.RequestModels.CommandRequestModels.ListDefinition;
using Application.ResponseModels.CommandResponseModels.ListDefinition;
using AutoMapper;
using Domain.Entities.ListAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.CommandHandlers.ListDefinition
{
    public class CreateListDefinitionCommandHandler : IRequestHandler<CreateListDefinitionCommandRequest, CreateListDefinitionCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateListDefinitionCommandHandler> _logger;
        private readonly IGenericRepositoryAsync<Domain.Entities.ListAggregate.ListDefinition> _listDRepository;
        private readonly IGenericRepositoryAsync<Domain.Entities.ListAggregate.ListTenantWorkflow> _listTWRepository;

        public CreateListDefinitionCommandHandler(IMapper mapper,
                                                  ILogger<CreateListDefinitionCommandHandler> logger,
                                                  IGenericRepositoryAsync<Domain.Entities.ListAggregate.ListDefinition> listDRepository,
                                                  IGenericRepositoryAsync<Domain.Entities.ListAggregate.ListTenantWorkflow> listTWRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _listDRepository = listDRepository;
            _listTWRepository = listTWRepository;
        }

        public async Task<CreateListDefinitionCommandResponse> Handle(CreateListDefinitionCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new CreateListDefinitionCommandResponse();

                var listDefinition = _mapper.Map<Domain.Entities.ListAggregate.ListDefinition>(request.ListDefinition);

                await _listDRepository.AddAsync(listDefinition, cancellationToken);

                var listTW = new ListTenantWorkflow
                {
                    ListId = listDefinition.Id,
                    TenantId = request.ListDefinition.TenantId,
                    WorkflowId = request.ListDefinition.DynamicFormId
                };

                var res = await _listTWRepository.AddAsync(listTW);

                response.ListDefinition = _mapper.Map<ListDefinitionDto>(listDefinition);

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
