using Application.Dto;
using Application.Interfaces.Repositories;
using Application.RequestModels.CommandRequestModels;
using Application.ResponseModels.CommandResponseModels;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.CommandHandlers.DynamicForm
{
    public class CreateDynamicFormCommandHandler : IRequestHandler<CreateDynamicFormCommandRequest, CreateDynamicFormCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateDynamicFormCommandHandler> _logger;
        private readonly IDynamicFormRepository _repository;


        public CreateDynamicFormCommandHandler(IMapper mapper, IDynamicFormRepository repository, ILogger<CreateDynamicFormCommandHandler> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public async Task<CreateDynamicFormCommandResponse> Handle(CreateDynamicFormCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new CreateDynamicFormCommandResponse();

                Domain.Entities.DynamicFormAggregate.DynamicForm dynamicForm = new Domain.Entities.DynamicFormAggregate.DynamicForm();

                dynamicForm.Name = request.Name;
                dynamicForm.Description = request.Description;
                dynamicForm.PlanId = request.PlanId;
                dynamicForm.Version = 1;
                dynamicForm.State = request.State;
                dynamicForm.MaxVersion = 1;


                await _repository.AddAsync(dynamicForm);

                response.dynamicForm = _mapper.Map<DynamicFormDto>(dynamicForm);
                return await Task.Run(() => response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                throw;
            }
        }
    }
}
