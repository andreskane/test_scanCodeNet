using Application.Dto;
using Application.Interfaces.Documental;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities.DynamicFormAggregate;
using Domain.Enums;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class DynamicFormService : IDynamicFormService
    {
        private readonly IDynamicFormItemRepository _dynamicFormItemRepository;
        private readonly IDynamicFormRepository _dynamicFormRepository;
        private readonly IDynamicFormComponentRuleRepository _dynamicFormComponentRuleRepository;
        private readonly IDocDynamicFormRepository _docDynamicFormRepository;

        private readonly IMapper _mapper;
        private readonly ILogger<DynamicFormService> _logger;


        public DynamicFormService(
           IMapper mapper, ILogger<DynamicFormService> logger,
           IDynamicFormRepository dynamicFormRepository,
           IDynamicFormComponentRuleRepository dynamicFormComponentRuleRepository,
           IDocDynamicFormRepository docDynamicFormRepository,

           IDynamicFormItemRepository dynamicFormItemRepository


            )

        {
            _mapper = mapper;
            _dynamicFormRepository = dynamicFormRepository;
            _dynamicFormComponentRuleRepository = dynamicFormComponentRuleRepository;
            _docDynamicFormRepository = docDynamicFormRepository;
            _logger = logger;
            _dynamicFormItemRepository = dynamicFormItemRepository;

        }



        public async Task<DynamicFormItem> CreateDynamicFormItem(DynamicFormItemDto dynamicForm, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<DynamicFormItem> UpdateDynamicFormItem(DynamicFormItemDto dynamicForm, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        public async Task<DynamicFormItem> CopyDynamicFormItem(DynamicFormItem dinamicFormItem, CancellationToken cancellationToken)
        {
            DynamicFormItem response = new DynamicFormItem();

            try
            {

                var df = _dynamicFormRepository.GetByIdAsync((long)dinamicFormItem.DynamicFormId);

                if (df != null)
                {



                    //get the last version of the dinamic form

                    var versionPublish = dinamicFormItem.Version;
                    var newDinamicFormItem = new DynamicFormItem();
                    newDinamicFormItem = dinamicFormItem;
                    newDinamicFormItem.Id = 0;
                    newDinamicFormItem.Version = df.Result.MaxVersion + 1;
                    newDinamicFormItem.Status = DynamicFormStatusEnum.UnPublished;

                    //new codeflow

                    var IdSecuence = await _docDynamicFormRepository.DuplicateDynamicForm(dinamicFormItem.CodeFlow);
                    newDinamicFormItem.CodeFlow = IdSecuence;

                    response = await _dynamicFormItemRepository.AddAsync(newDinamicFormItem, cancellationToken);

                    //     df.Result.Version = versionPublish;
                    df.Result.MaxVersion = newDinamicFormItem.Version;
                    df.Result.State = DynamicFormStatusEnum.Published;
                    df.Result.CodeFlowActive = dinamicFormItem.CodeFlow;
                    await _dynamicFormRepository.UpdateAsync(df.Result, cancellationToken);


                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when copy Dynamic Form Item {ErrorMessage}", ex.Message);
                throw;
            }

            return response;
        }


        public async Task<DynamicFormItem> SetVersionDynamicFormItem(DynamicFormItem dinamicFormItem, CancellationToken cancellationToken)
        {
            DynamicFormItem response = new DynamicFormItem();

            try
            {

                var df = _dynamicFormRepository.GetByIdAsync((long)dinamicFormItem.DynamicFormId);

                if (df != null)
                {



                    //get the last version of the dinamic form

                    var versionPublish = dinamicFormItem.Version;

                    df.Result.Version = versionPublish;

                    await _dynamicFormRepository.UpdateAsync(df.Result, cancellationToken);


                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when Change version Dynamic Form Item {ErrorMessage}", ex.Message);
                throw;
            }

            return response;
        }


    }
}
