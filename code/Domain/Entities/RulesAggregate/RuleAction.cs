using Domain.Entities.Common;
using Domain.Enums;

namespace Domain.Entities.RulesAggregate
{
    public class RuleAction : BaseAuditableEntity
    {
        public RuleType ActionType { get; set; }
        public RequestTypeEnums? RequestType { get; set; }
        public RequestByEnums? RequestBy { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public bool IsActive { get; set; }
        public string Url { get; set; }
        public string WhenScript { get; set; }
        public string ThenScript { get; set; }
        public int Order { get; set; }
        public Int64 RuleId { get; set; }
        public virtual RuleDynamic Rule { get; set; }
        public IList<ActionParameter> Parameters { get; set; }

    }
}
