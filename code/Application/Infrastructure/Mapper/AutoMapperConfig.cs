using Application.Dto;
using Application.Dto.Params.DynamicFormItem;
using Application.RequestModels.Extensions;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.DynamicFormAggregate;
using Domain.Entities.Layout;
using Domain.Entities.ListAggregate;
using Domain.Entities.RulesAggregate;

namespace Application.Infrastructure.Mapper;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<DynamicFormItemLitleDto, DynamicFormItem>()
            .ForMember(d => d.Id, o => o.MapFrom(src => src.DynamicFormItemId))

            .ReverseMap();


        CreateMap<DynamicFormItemTemplateDto, DynamicFormItem>()
          .ForMember(d => d.Id, o => o.MapFrom(src => src.DynamicFormTemplateItemId))
            .ReverseMap();



        CreateMap<DynamicForm, DynamicFormDto>() 
            .ForMember(x => x.ListDynamicForms, opt => opt.MapFrom(x => x.FlowList));

        CreateMap<DynamicFormDto, DynamicForm>()
        .ForMember(d => d.State, o => o.MapFrom(src => src.State.ToString()))
        .ForMember(x => x.FlowList, opt => opt.MapFrom(x => x.ListDynamicForms));

        CreateMap<PaginatedList<DynamicForm>, PaginatedList<DynamicFormDto>>();



        CreateMap<Properties, PropertiesBaseDto>();

       
        CreateMap<WorkflowItem, DynamicFormComponentRule>()
            
             .ForMember(dest => dest.ComponentPropertyId, opt => opt.MapFrom(src => src.properties.inputId))
            .ForMember(dest => dest.ComponentName, opt => opt.MapFrom(src => src.name))
            .ForMember(dest => dest.DataType, opt => opt.MapFrom(src => src.type))
            .ForMember(dest => dest.RuleId, opt => opt.MapFrom(src => src.properties.selectedRule.id))
            .ReverseMap();

        CreateMap<DynamicFormComponentRule, DynamicFormComponentRuleDto>()
            .ForMember(dest => dest.DynamicForm, opt => opt.MapFrom(src => src.DynamicFormItem.DynamicForm.Name))
            .ForMember(dest => dest.DynamicFormItemId, opt => opt.MapFrom(src => src.DynamicFormItem.Id))
            .ReverseMap();







        CreateMap<DynamicFormTemplate, DynamicFormTemplateDto>()
             
            .ForMember(x => x.ListDynamicForms, opt => opt.MapFrom(x => x.FlowList)).ReverseMap();

       
        CreateMap<TemplateComponentDto, TemplateComponent>().ReverseMap();

        CreateMap<BulkProcessDto, BulkProcess>();
        CreateMap<BulkProcess, BulkProcessDto>();
        CreateMap<BulckComponent, BulckComponentDto>();
        CreateMap<BulckComponentDto, BulckComponent>();






        CreateMap<DynamicFormItemDto, DynamicFormItem>()
            .ForMember(x => x.DynamicFormId, opt => opt.Ignore())
            .ForMember(x => x.DynamicFormTemplateId, opt => opt.Ignore());
        CreateMap<DynamicFormItem, DynamicFormItemDto>();


        CreateMap<DynamicFormPlanDto, DynamicFormPlan>().ReverseMap();




        CreateMap<PaginatedList<DynamicFormItem>, PaginatedList<DynamicFormItemDto>>();

        CreateMap<ListDefinitionDto, ListDefinition>();
        CreateMap<ListDefinition, ListDefinitionDto>();

        CreateMap<ListValue, ListValueDto>();
        CreateMap<ListValueDto, ListValue>();

        CreateMap<ListValueHeaders, ListDefinition>();
        CreateMap<ListDefinition, ListValueHeaders>();

        CreateMap<ListValue, ListValue>();

        CreateMap<RuleActionDto, RuleAction>();
        CreateMap<RuleAction, RuleActionDto>();

        CreateMap<RuleDynamic, RuleDto>();
        CreateMap<RuleDto, RuleDto>();


        CreateMap<ActionParameter, ActionParameterDto>();
        CreateMap<ActionParameterDto, ActionParameter>();







        //custom mapping


        CreateMap<RootObject, RootObjectDto>();
        CreateMap<DocDynamicForm, DocDynamicFormDto>();
        CreateMap<WorkflowItem, WorkflowItemDto>()
            .ForMember(dest => dest.properties,
                       opt => opt.MapFrom(src => MapToPropertiesDto(src)));

        // Configura mapeos específicos para cada subtipo de PropertiesBaseDto
        CreateMap<Properties, TitlePropertiesDto>()
            .IncludeBase<Properties, PropertiesBaseDto>();
        CreateMap<Properties, TextAreaPropertiesDto>().IncludeBase<Properties, PropertiesBaseDto>();
        CreateMap<Properties, DropDownPropertiesDto>().IncludeBase<Properties, PropertiesBaseDto>();
        CreateMap<Properties, InputPropertiesDto>().IncludeBase<Properties, PropertiesBaseDto>();
        CreateMap<Properties, ImagePropertiesDto>().IncludeBase<Properties, PropertiesBaseDto>();
        CreateMap<Properties, DatePropertiesDto>().IncludeBase<Properties, PropertiesBaseDto>();
        CreateMap<Properties, TitlePropertiesDto>().IncludeBase<Properties, PropertiesBaseDto>();
        CreateMap<Properties, TetxPropertiesDto>().IncludeBase<Properties, PropertiesBaseDto>();
        CreateMap<Properties, NumberPropertiesDto>().IncludeBase<Properties, PropertiesBaseDto>();
        CreateMap<Properties, LabelPropertiesDto>().IncludeBase<Properties, PropertiesBaseDto>();

    }

    private PropertiesBaseDto MapToPropertiesDto(WorkflowItem src)
    { 
        switch (src.type.ToUpper())
        {
            case "TEXTAREA":
                return MapToTextArea(src.properties);
            case "DROPDOWN":
                return MapToDropDown(src.properties);
            case "INPUT":
                return MapToInput(src.properties);
            case "DATE":
                return MapToDate(src.properties);
            case "TITLE":
                return MapToTitle(src.properties);
            case "TEXT":
                return MapToText(src.properties);
            case "NUMBER":
                return MapToNumber(src.properties);
            case "LABEL":
                return MapToLabel(src.properties);
            case "IMAGE":
                return MapToImage(src.properties);

            default:
                return MapToText(src.properties);
        }
    }
    private TextAreaPropertiesDto MapToTextArea(Properties src)
    {

        return new TextAreaPropertiesDto
        {

            description = src.description,
            inputId = src.inputId,
            value = src.value,
            label = src.label,
            color = src.color,
            fontSize = src.fontSize,
            fontWeight = src.fontWeight,
            placeholder = src.placeholder,

            required = src.required,
            disabled = src.disabled,
            isHidden = src.isHidden,
            minRows = src.minRows,
            maxRows = src.maxRows,
            selectedRule = new SelectedRuleDto
            {
                id = src.selectedRule.id,
                value = src.selectedRule.value,
                description = src.selectedRule.description
            }
        };


    }
    private DropDownPropertiesDto MapToDropDown(Properties src)
    {
        
        return new DropDownPropertiesDto
        {
            description = src.description,
            inputId = src.inputId,
            value = src.value,
            color = src.color,
            fontSize = src.fontSize,
            fontWeight = src.fontWeight,

            label = src.label,

            required = src.required,
            disabled = src.disabled,
            isHidden = src.isHidden,

            selectedRule = new SelectedRuleDto
            {
                id = src.selectedRule.id,
                value = src.selectedRule.value,
                description = src.selectedRule.description
            },
            options = src.options.Select(x => new OptionDto
            {
                value = x.value,
                viewValue = x.viewValue
            }).ToList(),
            selectedOption = new OptionDto
            {
                value = src.selectedOption.value,
                viewValue = src.selectedOption.viewValue
            }

        };
    }
    private InputPropertiesDto MapToInput(Properties src)
    {
         
        return new InputPropertiesDto
        {
            color = src.color,
            fontSize = src.fontSize,
            fontWeight = src.fontWeight,
            description = src.description,
            inputId = src.inputId,
            value = src.value,
            label = src.label,
            placeholder = src.placeholder,
            required = src.required,
            disabled = src.disabled,
            isHidden = src.isHidden,
            selectedRule = new SelectedRuleDto
            {
                id = src.selectedRule.id,
                value = src.selectedRule.value,
                description = src.selectedRule.description
            }

        };
    }

    private ImagePropertiesDto MapToImage(Properties src)
    {
        
        return new ImagePropertiesDto
        {
            alt = src.alt,
            description = src.description,
            inputId = src.inputId,
            src = src.src,
            maxWidth = src.maxWidth,
            maxHeight = src.maxHeight,
            isHidden = src.isHidden,
            selectedRule = new SelectedRuleDto
            {
                id = src.selectedRule.id,
                value = src.selectedRule.value,
                description = src.selectedRule.description

            },
            value = src.value,
            label = src.label,
            required = src.required,
            disabled = src.disabled,
            color = src.color,
            fontSize = src.fontSize,
            fontWeight = src.fontWeight



        };
    }
    private DatePropertiesDto MapToDate(Properties src)
    {
        
        return new DatePropertiesDto
        {
            color = src.color,
            fontSize = src.fontSize,
            fontWeight = src.fontWeight,
            description = src.description,
            inputId = src.inputId,
            value = src.value,
            label = src.label,
            required = src.required,
            disabled = src.disabled,
            isHidden = src.isHidden,
            selectedRule = new SelectedRuleDto() { id = src.selectedRule.id, value = src.selectedRule.value, description = src.selectedRule.description },



        };
    }

    private TitlePropertiesDto MapToTitle(Properties src)
    {
       
        return new TitlePropertiesDto
        {
            color = src.color,
            fontSize = src.fontSize,
            fontWeight = src.fontWeight,
            description = src.description,
            inputId = src.inputId,
            value = src.value,
            label = src.label,
            required = src.required,
            disabled = src.disabled,
            isHidden = src.isHidden,
            selectedRule = new SelectedRuleDto
            {
                id = src.selectedRule.id,
                value = src.selectedRule.value,
                description = src.selectedRule.description

            }

        };
    }
    private TetxPropertiesDto MapToText(Properties src)
    {
         
        return new TetxPropertiesDto
        {
            color = src.color,
            fontSize = src.fontSize,
            fontWeight = src.fontWeight,
            description = src.description,
            inputId = src.inputId,
            value = src.value,
            label = src.label,
            required = src.required,
            disabled = src.disabled,
            isHidden = src.isHidden,
            selectedRule = new SelectedRuleDto
            {
                id = src.selectedRule.id,
                value = src.selectedRule.value,
                description = src.selectedRule.description
            }

        };
    }

    private NumberPropertiesDto MapToNumber(Properties src)
    {
     
        return new NumberPropertiesDto
        {
            color = src.color,
            fontSize = src.fontSize,
            fontWeight = src.fontWeight,
            description = src.description,
            inputId = src.inputId,
            value = src.value,
            label = src.label,
            required = src.required,
            disabled = src.disabled,
            isHidden = src.isHidden,
            selectedRule = new SelectedRuleDto
            {
                id = src.selectedRule.id,
                value = src.selectedRule.value,
                description = src.selectedRule.description
            }
        };
    }
    private LabelPropertiesDto MapToLabel(Properties src)
    {
        
        return new LabelPropertiesDto
        {
            color = src.color,
            fontSize = src.fontSize,
            fontWeight = src.fontWeight,
            description = src.description,
            inputId = src.inputId,
            value = src.value,
            label = src.label,
            required = src.required,
            disabled = src.disabled,
            isHidden = src.isHidden,
            selectedRule = new SelectedRuleDto
            {
                id = src.selectedRule.id,
                value = src.selectedRule.value,
                description = src.selectedRule.description
            }
        };
    }
}
