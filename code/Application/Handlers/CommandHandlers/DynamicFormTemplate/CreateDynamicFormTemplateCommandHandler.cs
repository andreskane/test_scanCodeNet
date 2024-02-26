using Application.Dto;
using Application.Interfaces.Documental;
using Application.Interfaces.Repositories;
using Application.RequestModels.CommandRequestModels;
using Application.ResponseModels.CommandResponseModels;
using AutoMapper;
using Domain.Entities.Layout;
using MediatR;
using Microsoft.Extensions.Logging;


namespace Application.Handlers.CommandHandlers.WorkflowTemplate
{
    public class CreateDynamicFormTemplateCommandHandler : IRequestHandler<CreateDynamicFormTemplateCommandRequest, CreateDynamicFormTemplateCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CreateDynamicFormTemplateCommandHandler> _logger;
        private readonly IGenericRepositoryAsync<Domain.Entities.DynamicFormAggregate.DynamicFormTemplate> _repository;
        private readonly IGenericRepositoryAsync<Domain.Entities.DynamicFormAggregate.DynamicFormItem> _repositoryItem;
        private readonly IDocDynamicFormRepository _docDynamicFormRepository;

        public CreateDynamicFormTemplateCommandHandler(IMapper mapper,
            IGenericRepositoryAsync<Domain.Entities.DynamicFormAggregate.DynamicFormTemplate> repository,
            ILogger<CreateDynamicFormTemplateCommandHandler> logger,
            IGenericRepositoryAsync<Domain.Entities.DynamicFormAggregate.DynamicFormItem> repositoryItem,
            IDocDynamicFormRepository docDynamicFormRepository

            )
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _repositoryItem = repositoryItem;
            _docDynamicFormRepository = docDynamicFormRepository;
        }

        public async Task<CreateDynamicFormTemplateCommandResponse> Handle(CreateDynamicFormTemplateCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new CreateDynamicFormTemplateCommandResponse();

                var dynamicForm = new Domain.Entities.DynamicFormAggregate.DynamicFormTemplate();

                dynamicForm.Name = request.Name;
                dynamicForm.Description = request.Description;
 
                dynamicForm.State = request.State;

                var res = await _repository.AddAsync(dynamicForm);

                response.DynamicFormTemplate = _mapper.Map<DynamicFormTemplateDto>(dynamicForm);

                var flow = new Domain.Entities.DynamicFormAggregate.DynamicFormItem()
                {
                    DynamicFormTemplateId = res.Id,
                    Layout = request.Layout,
                  

                    Code = "-",
                    Description = request.Description,
                    
                    Status = request.State,
                  

                }
                   ;


                string IdSecuence = string.Empty;
                List<WorkflowItem> pages = Newtonsoft.Json.JsonConvert.DeserializeObject<List<WorkflowItem>>(flow.Layout);
                IdSecuence = await _docDynamicFormRepository.InsertTemplate(new DocLayoutTemplate() { Pages = pages });

                flow.Version = 1;
                flow.CodeFlow = IdSecuence;


                var resFlow = _repositoryItem.AddAsync(flow).Result;

                response.DynamicFormTemplate.ListDynamicForms = new List<DynamicFormItemTemplateDto>();
                response.DynamicFormTemplate.ListDynamicForms.Add(_mapper.Map<DynamicFormItemTemplateDto>(resFlow));

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
