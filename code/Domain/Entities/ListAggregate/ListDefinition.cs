using Domain.Entities.Common;

namespace Domain.Entities.ListAggregate
{
    public class ListDefinition : BaseAuditableEntity
    {
        public string ListName { get; set; }
        public string Description { get; set; }
        public string KeyName { get; set; }
        public string ValueName { get; set; }
        public string DataType { get; set; }
        public List<ListTenantWorkflow> ListsTenantsWorkflows { get; set; }
        public List<ListValue> ListValues { get; set; }
    }
}
