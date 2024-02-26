using Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.DynamicFormAggregate
{
    public class DynamicFormProductAttributes : BaseAuditableEntity
    {
        public string ZipCode { get; set; }
        public long ProductId { get; set; }
        public long DynamicFormId { get; set; }
        public string AtributteCode { get; set; }
        public DataType DataType { get; set; }
        public string DefaultValue { get; set; }
        public string ExtraInfo { get; set; }
        public string Metadata { get; set; }
        public bool Optional { get; set; }
        public virtual DynamicFormItem DynamicForm { get; set; }
    }
}
