using Domain.Enums;

namespace Application.Dto
{
    public class DynamicFormDto
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DynamicFormStatusEnum State { get; set; }

        public Int64 PlanId { get; set; }
        public string Version { get; set; }
        public DateTime Created { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public string? LastModifiedBy { get; set; }

        public List<DynamicFormItemLitleDto> ListDynamicForms { get; set; }

    }
}
