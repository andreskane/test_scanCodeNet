using Domain.Entities.Common;
using Domain.Enums;

namespace Application.Dto
{
    public class BulkProcessRequestDto
    {
        public Int64? id { get; set; }
        public string Name { get; set; }
        public ProcessTypeEnum ProcessType { get; set; }
        public ProcessStatusEnum Status { get; set; }
        public string ErrorMessage { get; set; }
        public PlacementPreferenceEnum PlacementPreference { get; set; }
        public string ComponentOfReference { get; set; }




        public IList<BulckComponentDto> ComponentListItems { get; set; }



    }
    public class BulkProcessDto
    {
        public Int64? id { get; set; }
        public string Name { get; set; }
        public ProcessTypeEnum ProcessType { get; set; }
        public ProcessStatusEnum Status { get; set; }
        public string ErrorMessage { get; set; }
        public PlacementPreferenceEnum PlacementPreference { get; set; }
        public string ComponentOfReference { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }



        public IList<BulckComponentDto> ComponentListItems { get; set; }



    }
    public class DynamicFormBulckProcessDto : BaseAuditableEntity
    {
        public Int64 DynamicFormId { get; set; }
        public Int64 BulkProcessId { get; set; }

    }
    public class BulckComponentDto
    {
        public Int64? Id { get; set; }
        public string Name { get; set; }
        public componentTypeEnum typeComponent { get; set; }
        public Int32 Order { get; set; }
        public string Description { get; set; }
        public string InputId { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
        public bool IsHidden { get; set; }
        public bool Required { get; set; }

        public Int64 BulkProcessId { get; set; }
    }



}
