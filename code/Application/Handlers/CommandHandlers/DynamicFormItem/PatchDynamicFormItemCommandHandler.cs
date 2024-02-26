using Application.Dto;
using Application.Interfaces.Documental;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.RequestModels.CommandRequestModels.DynamicFormItem;
using Application.ResponseModels.CommandResponseModels.DynamicFormItem;
using AutoMapper;
using Domain.Entities.Layout;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.CommandHandlers.DynamicFormItem
{
    public class PatchDynamicFormItemCommandHandler : IRequestHandler<PatchDynamicFormItemCommandRequest, PatchDynamicFormItemCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PatchDynamicFormItemCommandHandler> _logger;
        private readonly IDynamicFormItemRepository _repository;
        private readonly IDynamicFormRepository _dynamicFormRepository;
        private readonly IDocDynamicFormRepository _docDynamicFormRepository;
        private readonly IDynamicFormComponentRuleRepository _dynamicFormComponentRuleRepository;
        private readonly IDynamicFormService _dynamicFormService;
        private readonly IFormComponentRuleService _formComponentRuleService;
        public PatchDynamicFormItemCommandHandler(IMapper mapper,
                     IDocDynamicFormRepository docDynamicFormRepository,
                     IDynamicFormService dynamicFormService,
        IDynamicFormItemRepository repository, ILogger<PatchDynamicFormItemCommandHandler> logger,
        IDynamicFormRepository dynamicFormRepository, IDynamicFormComponentRuleRepository dynamicFormComponentRuleRepository,
            IFormComponentRuleService formComponentRuleService


            )
        {
            _docDynamicFormRepository = docDynamicFormRepository;
            _dynamicFormRepository = dynamicFormRepository;
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _dynamicFormComponentRuleRepository = dynamicFormComponentRuleRepository;
            _dynamicFormService = dynamicFormService;
            _formComponentRuleService = formComponentRuleService;
        }

        public async Task<PatchDynamicFormItemCommandResponse> Handle(PatchDynamicFormItemCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = new PatchDynamicFormItemCommandResponse();

                var dinamicFormItem = await _repository.GetDynamicFormItemByIdWithDynamicForm(request.DynamicFormItemId, cancellationToken);

                if (dinamicFormItem == null)
                    return null;


                dinamicFormItem.Status = request.Status;
 
                var ListDinamicFormItem = await _repository.GetDynamicFormItemByDynamicFormId((long)dinamicFormItem.DynamicFormId, cancellationToken);

                foreach (var item in ListDinamicFormItem)
                {
                    if (item.Id != dinamicFormItem.Id)
                    {
                        item.Status = DynamicFormStatusEnum.UnPublished;
                        await _repository.UpdateAsync(item, cancellationToken);
                    }
                }


                await _repository.UpdateAsync(dinamicFormItem, cancellationToken);

                var layout = await _docDynamicFormRepository.GetDynamicFormByKey(dinamicFormItem.CodeFlow);
                List<DocDynamicForm> pages = layout.Pages;

                if (request.Status == DynamicFormStatusEnum.Published)
                {
                    await _formComponentRuleService.GenerateFormComponentList(pages, dinamicFormItem.Id);
                    dinamicFormItem = await _dynamicFormService.SetVersionDynamicFormItem(dinamicFormItem, cancellationToken);
                }

                response.DynamicFormItem = _mapper.Map<DynamicFormItemDto>(dinamicFormItem);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                throw ex;
            }
        }
    }
}
