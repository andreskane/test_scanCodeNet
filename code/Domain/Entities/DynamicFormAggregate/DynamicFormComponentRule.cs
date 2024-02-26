using Domain.Entities.Common;

namespace Domain.Entities.DynamicFormAggregate
{
    public class DynamicFormComponentRule : BaseAuditableEntity
    {
        public Int64 DynamicFormItemId { get; set; }
        public string ComponentName { get; set; }
        public string ComponentPropertyId { get; set; }
        public string DataType { get; set; }
        public String RuleId { get; set; }
        public virtual DynamicFormItem DynamicFormItem { get; set; }
    }
}
