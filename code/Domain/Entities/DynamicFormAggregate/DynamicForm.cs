using Domain.Entities.Common;
using Domain.Entities.ListAggregate;
using Domain.Entities.RulesAggregate;
using Domain.Enums;

namespace Domain.Entities.DynamicFormAggregate
{
    public class DynamicForm : BaseAuditableEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DynamicFormStatusEnum State { get; set; }
        public Int32 MaxVersion { get; set; }
        public Int32 Version { get; set; }
        public string CodeFlowActive { get; set; }
        public Int64 PlanId { get; set; }
        public IList<ListTenantWorkflow> ListTenantWorkflows { get; set; }
        public List<DynamicFormItem> FlowList { get; set; }
        public IList<DynamicFormRule> DynamicFormRules { get; set; }

        public IList<DynamicFormBulckProcess> DynamicFormBulkProcess { get; set; }

        public ICollection<DynamicFormPlan> DynamicFormPlans { get; private set; } = new List<DynamicFormPlan>();


    }
}
