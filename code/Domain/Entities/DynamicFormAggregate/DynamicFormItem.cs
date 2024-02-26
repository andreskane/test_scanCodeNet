using Domain.Entities.Common;
using Domain.Enums;

namespace Domain.Entities.DynamicFormAggregate
{
    public class DynamicFormItem : BaseAuditableEntity
    {

        public string Code { get; set; }
        public string? Layout { get; set; }
        public Int32 Version { get; set; }
        public DynamicFormStatusEnum Status { get; set; }
        public string? CodeFlow { get; set; }
        public string Description { get; set; }
        public DynamicForm? DynamicForm { get; set; }
        public DynamicFormTemplate? DynamicFormTemplate { get; set; }

        public long? DynamicFormId { get; set; }
        public long? DynamicFormTemplateId { get; set; }

        //  public virtual List<DynamicFormProductAttributes> Attributes { get; set; }
        public void IncrementVersion()
        {
            // Incrementa el campo de versión
            Version++;
        }
    }
}
