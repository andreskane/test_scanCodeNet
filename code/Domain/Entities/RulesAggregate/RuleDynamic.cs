using Domain.Entities.Common;

namespace Domain.Entities.RulesAggregate
{
    public class RuleDynamic : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string? KeyDocument { get; set; }

        public bool Enabled { get; set; }
        public IList<RuleAction> Actions { get; set; }
        public IList<DynamicFormRule> DynamicFormRules { get; set; }
    }
}
