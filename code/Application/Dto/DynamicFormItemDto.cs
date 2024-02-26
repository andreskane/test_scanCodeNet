using Domain.Enums;

namespace Application.Dto
{
    public class DynamicFormItemDto
    {
        public Int64 Id { get; set; }
        public Int64? DynamicFormId { get; set; }
        public Int64? DynamicFormTemplateId { get; set; }

        public string Layout { get; set; }
        public Int32 Version { get; set; }
        public DynamicFormStatusEnum Status { get; set; }
        public string CodeFlow { get; set; }
        public string Description { get; set; }
        public DateTime? LastModified { get; set; }

        public string? LastModifiedBy { get; set; }
    }

    public class DynamicFormItemLitleDto
    {
        public Int64 DynamicFormItemId { get; set; }
        public Int64 DynamicFormId { get; set; }

        public string CodeFlow { get; set; }
        public Int32 Version { get; set; }
        public DynamicFormStatusEnum Status { get; set; }
        public string Description { get; set; }
    }


    public class DynamicFormItemTemplateDto
    {
        public Int64 DynamicFormTemplateItemId { get; set; }
        public Int64 DynamicFormTemplateId { get; set; }

        public string Layout { get; set; }
        public Int32 Version { get; set; }
        public DynamicFormStatusEnum Status { get; set; }
        public string CodeFlow { get; set; }
        public string Description { get; set; }
    }

    public class DynamicFormItemTemplateLitleDto
    {
        public Int64 DynamicFormTemplateItemId { get; set; }
        public Int64 DynamicFormTemplateId { get; set; }

        public string CodeFlow { get; set; }
        public Int32 Version { get; set; }
        public DynamicFormStatusEnum Status { get; set; }
        public string Description { get; set; }
    }

}
