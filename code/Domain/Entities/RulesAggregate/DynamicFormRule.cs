using Domain.Entities.Common;
using Domain.Entities.DynamicFormAggregate;

namespace Domain.Entities.RulesAggregate
{
    public class DynamicFormRule : BaseAuditableEntity
    {
        public Int64 DynamicFormId { get; set; }
        public Int64 RuleId { get; set; }
        public virtual DynamicForm DynamicForm { get; set; }
        public virtual RuleDynamic Rule { get; set; }
    }
}
