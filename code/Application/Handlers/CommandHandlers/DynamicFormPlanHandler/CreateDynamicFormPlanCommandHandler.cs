using Application.Dto;
using Application.Interfaces.Repositories;
using Application.RequestModels.CommandRequestModels.DynamicFormPlan;
using Application.ResponseModels.CommandResponseModels.DynamicFormPlan;
using AutoMapper;
using Domain.Entities.DynamicFormAggregate;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.CommandHandlers.DynamicFormPlanHandler
{
    public class CreateDynamicFormPlanCommandHandler : IRequestHandler<CreateDynamicFormPlanCommandRequest, CreateDynamicFormPlanCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDynamicFormPlanRepository _repository;
        private readonly ILogger<CreateDynamicFormPlanCommandHandler> _logger;


        public CreateDynamicFormPlanCommandHandler(IMapper mapper, IDynamicFormPlanRepository repository, ILogger<CreateDynamicFormPlanCommandHandler> logger)
        {
            _mapper = mapper;

            _repository = repository;
            _logger = logger;
        }

        public async Task<CreateDynamicFormPlanCommandResponse> Handle(CreateDynamicFormPlanCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new CreateDynamicFormPlanCommandResponse();
                var dynamicForm = await _repository.GetPublishedPlanAsync(request.DynamicFormId);

                if (dynamicForm?.PlanId == request.PlanId)
                    return response;

                if (dynamicForm != null)
                {
                    dynamicForm.Status = DynamicFormStatusEnum.UnPublished; 
                    await _repository.UpdateAsync(dynamicForm, cancellationToken);
                }


                var dynamicoFormPlan = new DynamicFormPlan()
                {
                    DynamicFormId = request.DynamicFormId,
                    PlanId = request.PlanId,
                    Status = DynamicFormStatusEnum.Published 
                };

                await _repository.AddAsync(dynamicoFormPlan, cancellationToken);

                response.DynamicFormPlan = _mapper.Map<DynamicFormPlanDto>(dynamicoFormPlan);
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