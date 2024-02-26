using Domain.Entities.Common;
using Domain.Enums;

namespace Domain.Entities.DynamicFormAggregate
{
    public class DynamicFormPlan : BaseAuditableEntity
    {
        public Int64 DynamicFormId { get; set; }
        public DynamicForm DynamicForm { get; set; }

        public Int64 PlanId { get; set; }

        public DynamicFormStatusEnum Status { get; set; }
    }
}
