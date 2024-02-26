using Domain.Entities.Common;
using Domain.Entities.DynamicFormAggregate;

namespace Domain.Entities.ListAggregate
{
    public class ListTenantWorkflow : BaseAuditableEntity
    {
        public Int64 ListId { get; set; }
        public string? TenantId { get; set; }
        public Int64? WorkflowId { get; set; }
        public ListDefinition ListDefinition { get; set; }
        public DynamicForm? Workflow { get; set; }
    }
}
