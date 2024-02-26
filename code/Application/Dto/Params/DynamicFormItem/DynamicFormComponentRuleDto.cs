namespace Application.Dto.Params.DynamicFormItem
{
    public class DynamicFormComponentRuleDto
    {
        public Int64 DynamicFormItemId { get; set; }
        public String DynamicForm { get; set; }
        public string ComponentName { get; set; }
        public string ComponentPropertyId { get; set; }
        public string DataType { get; set; }
        public String RuleId { get; set; }
    }
}
