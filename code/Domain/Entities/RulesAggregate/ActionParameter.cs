using Domain.Entities.Common;

namespace Domain.Entities.RulesAggregate
{
    public class ActionParameter : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public bool IsRequest { get; set; }
        public Int64 RuleActtioId { get; set; }
        public virtual RuleAction RuleAction { get; set; }
    }
}
