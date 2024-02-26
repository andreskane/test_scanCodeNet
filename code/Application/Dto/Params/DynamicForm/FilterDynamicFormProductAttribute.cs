using ConnectureOS.Framework.Message;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Application.Dto.Params.DynamicForm
{
    public class FilterDynamicFormProductAttribute : PaginatedRequestDto
    {
        [DataMember]
        [Required(ErrorMessage = ErrorMessageText.Required)]
        public Int64? Id { get; set; }
    }
}
