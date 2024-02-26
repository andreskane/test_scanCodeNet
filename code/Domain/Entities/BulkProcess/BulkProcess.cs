using Domain.Entities.Common;
using Domain.Entities.DynamicFormAggregate;
using Domain.Entities.Layout;
using Domain.Enums;

namespace Domain.Entities
{
    public class BulkProcess : BaseAuditableEntity
    {
        public string Name { get; set; }
        public ProcessTypeEnum ProcessType { get; set; }
        public ProcessStatusEnum Status { get; set; }
        public string? ErrorMessage { get; set; }
        public PlacementPreferenceEnum? PlacementPreference { get; set; }
        public string? ComponentOfReference { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public IList<DynamicFormBulckProcess> DynamicFormListItems { get; set; }

        public IList<BulckComponent> ComponentListItems { get; set; }




    }

    public class DynamicFormBulckProcess : BaseAuditableEntity
    {
        public Int64 DynamicFormId { get; set; }
        public Int64 BulkProcessId { get; set; }
        public virtual DynamicForm DynamicForm { get; set; }
        public virtual BulkProcess BulkProcess { get; set; }
    }



    public class BulckComponent : BaseAuditableEntity
    {
        public string Name { get; set; }
        public componentTypeEnum typeComponent { get; set; }
        public Int32 Order { get; set; }
        public string? Description { get; set; }
        public string InputId { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
        public bool IsHidden { get; set; }
        public bool Required { get; set; }
        public SelectedRule SelectedRule { get; set; }

        public virtual BulkProcess BulkProcess { get; set; }
        public Int64 BulkProcessId { get; set; }
    }



}
