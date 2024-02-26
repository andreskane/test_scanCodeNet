using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities.DynamicFormAggregate;
using Domain.Entities.Layout;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services
{

    public class FormComponentRuleService : IFormComponentRuleService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DynamicFormService> _logger;
        private readonly IDynamicFormComponentRuleRepository _dynamicFormComponentRuleRepository;
        public FormComponentRuleService(
              IMapper mapper,
              ILogger<DynamicFormService> logger
,
              IDynamicFormComponentRuleRepository dynamicFormComponentRuleRepository

            )
        {
            _mapper = mapper;
            _logger = logger;
            _dynamicFormComponentRuleRepository = dynamicFormComponentRuleRepository;
        }
        public async Task GenerateFormComponentList(List<DocDynamicForm> pages, long dinamicFormItemId)
        {


            try
            {
                var dynamicFormComponentRules = new List<DynamicFormComponentRule>();
                foreach (var page in pages)
                {
                    foreach (var workFlow in page.workflowTable)
                    {
                        if (workFlow.properties.inputId != null)
                        {

                            var dynamicFormComponentRule = new DynamicFormComponentRule()
                            {
                                ComponentName = workFlow.name,
                                ComponentPropertyId = workFlow.properties.inputId,
                                DataType = workFlow.type,
                                DynamicFormItemId = dinamicFormItemId,
                                RuleId = workFlow.properties.selectedRule.id

                            };
                            dynamicFormComponentRules.Add(dynamicFormComponentRule);
                        }
                    }

                }

                List<long> dynamicFormIds = dynamicFormComponentRules.Select(x => x.DynamicFormItemId).ToList();
                await _dynamicFormComponentRuleRepository.BulkDeleteByDynamicFormIdsAsync(dynamicFormIds);
                await _dynamicFormComponentRuleRepository.BulkAsync(dynamicFormComponentRules);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error saving or deleting dynamic form component rules to the database: {ErrorMessage}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.StackTrace);
                throw;
            }



        }
    }


}
