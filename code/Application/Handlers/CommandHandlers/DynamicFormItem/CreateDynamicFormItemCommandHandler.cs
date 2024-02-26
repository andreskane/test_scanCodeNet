using Application.Dto;
using Application.Interfaces.Documental;
using Application.Interfaces.Repositories;
using Application.RequestModels.CommandRequestModels;
using Application.ResponseModels.CommandResponseModels;
using AutoMapper;
using Domain.Entities.Layout;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.CommandHandlers.DynamicFormItem
{

    public class CreateDynamicFormItemCommandHandler : IRequestHandler<CreateDynamicFormItemCommandRequest, CreateDynamicFormItemCommandResponse>
    {

        private readonly IGenericRepositoryAsync<Domain.Entities.DynamicFormAggregate.DynamicFormItem> _repository;
        private readonly IDynamicFormRepository _dynamicFormRepository;
        private readonly IDynamicFormComponentRuleRepository _dynamicFormComponentRuleRepository;
        private readonly IDocDynamicFormRepository _docDynamicFormRepository;

        private readonly IMapper _mapper;
        private readonly ILogger<CreateDynamicFormItemCommandHandler> _logger;


        public CreateDynamicFormItemCommandHandler(IGenericRepositoryAsync<Domain.Entities.DynamicFormAggregate.DynamicFormItem> repository,
            IDynamicFormRepository dynamicFormRepository, IDocDynamicFormRepository docDynamicFormRepository,
            IDynamicFormComponentRuleRepository dynamicFormComponentRuleRepository, IMapper mapper,
            ILogger<CreateDynamicFormItemCommandHandler> logger)
        {
            _repository = repository;

            _docDynamicFormRepository = docDynamicFormRepository;
            _dynamicFormRepository = dynamicFormRepository;
            _dynamicFormComponentRuleRepository = dynamicFormComponentRuleRepository;

            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CreateDynamicFormItemCommandResponse> Handle(CreateDynamicFormItemCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new CreateDynamicFormItemCommandResponse();

                var workflowItem = _mapper.Map<Domain.Entities.DynamicFormAggregate.DynamicFormItem>(request.WDynamicForm);


                if (request.WDynamicForm.DynamicFormId != null)
                {
                    Domain.Entities.DynamicFormAggregate.DynamicForm df = await _dynamicFormRepository.GetByIdAsync((long)request.WDynamicForm.DynamicFormId);
                    df.Version = 1;
                }

                try
                {
                    string IdSecuence = string.Empty;
                    List<DocDynamicForm> pages = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DocDynamicForm>>(request.WDynamicForm.Layout);
                    IdSecuence = await _docDynamicFormRepository.InsertDynamicForm(new RootObject() { Pages = pages });
                    workflowItem.Version = 1;
                    workflowItem.CodeFlow = IdSecuence;

                    if (request.WDynamicForm.DynamicFormId != null)
                    {
                        workflowItem.DynamicFormId = request.WDynamicForm.DynamicFormId;
                    }
                    if (request.WDynamicForm.DynamicFormTemplateId != null)
                    {
                        workflowItem.DynamicFormTemplateId = request.WDynamicForm.DynamicFormTemplateId;
                    }


                    await _repository.AddAsync(workflowItem);
                    response.WDynamicForm = _mapper.Map<DynamicFormItemDto>(workflowItem);

                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError("Error saving dynamic form component rules to the database: {ErrorMessage}", ex.Message);
                    throw;
                }
                catch (Exception ex)
                {

                    _logger.LogError(ex.StackTrace);
                     throw;
                }


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
