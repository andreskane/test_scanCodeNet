using System.Runtime.Serialization;

namespace Application.Dto.Params.DynamicFormItem
{
    public class FilterDynamicFormComponent : PaginatedRequestDto
    {
        [DataMember]
        public String DynamicFormName { get; set; }

        [DataMember]
        public string ComponentName { get; set; }
    }
}
