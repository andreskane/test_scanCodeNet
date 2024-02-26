using Domain.Entities.Common;
using Domain.Entities.ListAggregate;
using Domain.Enums;

namespace Domain.Entities.DynamicFormAggregate
{
    public class DynamicFormTemplate : BaseAuditableEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DynamicFormStatusEnum State { get; set; }
        public string? CodeFlowActive { get; set; }
        public Int32 Version { get; set; }
        //  public Int64 PlanId { get; set; }

        //todo: see requirements for this
        public IList<ListTenantWorkflow> ListTenantWorkflows { get; set; }
        public List<DynamicFormItem> FlowList { get; set; }


    }
}
