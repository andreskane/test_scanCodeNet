using Application.Interfaces.Documental;
using Application.Interfaces.Repositories;
using Application.RequestModels.CommandRequestModels;
using Application.ResponseModels.CommandResponseModels;
using Application.Services.Template;
using Domain.Entities.Layout;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.CommandHandlers.DynamicFormItem
{

    public class UpdateDynamicFormItemCommandHandler : IRequestHandler<UpdateDynamicFormItemCommandRequest, UpdateDynamicFormItemCommandResponse>
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.DynamicFormAggregate.DynamicFormItem> _repository;
        private readonly IDocDynamicFormRepository _docDynamicFormRepository;

        private readonly ITemplateService _templateService;

        private readonly ILogger<UpdateDynamicFormItemCommandHandler> _logger;

        public UpdateDynamicFormItemCommandHandler(IGenericRepositoryAsync<Domain.Entities.DynamicFormAggregate.DynamicFormItem> repository,
            IDocDynamicFormRepository docDynamicFormRepository,
            ITemplateService templateService, ILogger<UpdateDynamicFormItemCommandHandler> logger)
        {
            _repository = repository;
            _docDynamicFormRepository = docDynamicFormRepository;

            _templateService = templateService;


            _logger = logger;
        }


        public async Task<UpdateDynamicFormItemCommandResponse> Handle(UpdateDynamicFormItemCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var dynamicForm = await _repository.GetByIdAsync(request.WDynamicForm.Id);
                if (dynamicForm == null)
                    return null;

                var DynamicFormId = dynamicForm.DynamicFormId;
                var oCodeFlow = dynamicForm.CodeFlow;
                var response = new UpdateDynamicFormItemCommandResponse();

                dynamicForm.Layout = request.WDynamicForm.Layout;

                dynamicForm.DynamicFormId = DynamicFormId;

                string IdSecuence = string.Empty;

                if (dynamicForm.DynamicFormTemplateId != null)
                {

                    try
                    {

                        List<WorkflowItem> pages = Newtonsoft.Json.JsonConvert.DeserializeObject<List<WorkflowItem>>(request.WDynamicForm.Layout);

                        pages.ForEach(objetoA => objetoA.templateId = (long)dynamicForm.DynamicFormTemplateId);

                        IdSecuence = await _docDynamicFormRepository.UpdateTemplate(new DocLayoutTemplate() { Pages = pages }, oCodeFlow);

                        dynamicForm.CodeFlow = IdSecuence;


                        await _templateService.Execute(dynamicForm.DynamicFormTemplateId.Value, new DocLayoutTemplate() { Pages = pages }, cancellationToken);
                    }
                    catch (Exception ex)
                    {

                        _logger.LogError(ex.StackTrace);
                        throw;
                    }

                }
                else
                {
                    try
                    {
                        List<DocDynamicForm> pages = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DocDynamicForm>>(request.WDynamicForm.Layout);
                        IdSecuence = await _docDynamicFormRepository.UpdateDynamicForm(new RootObject() { Pages = pages }, oCodeFlow);

                        dynamicForm.CodeFlow = IdSecuence;
                    }
                    catch (Exception ex)
                    {

                        _logger.LogError(ex.StackTrace);
                        throw;
                    }
                }



                await _repository.UpdateAsync(dynamicForm, cancellationToken);
                response.WDynamicForm = request.WDynamicForm;

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
