using Domain.Enums;
using System.Runtime.Serialization;

namespace Application.Dto.Params.DynamicForm
{
    public class FilterDynamicForm : PaginatedRequestDto
    {
        [DataMember]

      
        public String Name { get; set; }
        public DynamicFormStatusEnum? Status { get; set; }
        public long? PlanID { get; set; }
        public DynamicFormStatusEnum? State { get; set; }
        public Int32? Version { get; set; }
    }
}
