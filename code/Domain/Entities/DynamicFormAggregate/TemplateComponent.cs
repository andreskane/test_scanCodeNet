using Domain.Entities.Common;
using Domain.Enums;

namespace Domain.Entities.DynamicFormAggregate
{
    public class TemplateComponent : BaseEntity
    {

        public string Name { get; set; }
        public string? Description { get; set; }
        public componentTypeEnum DataType { get; set; }
        public string? Label { get; set; }
        public string? value { get; set; }
        public string? data { get; set; }
        public string? placeholder { get; set; }
        public string? groupID { get; set; }
        public Boolean IsRequired { get; set; }
        public Boolean IsHidden { get; set; }
    }
}
