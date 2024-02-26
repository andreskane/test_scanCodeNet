using ConnectureOS.Framework.Message;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Application.Dto.Params;

public class FilterPersonDto : PaginatedRequestDto
{
    [DataMember]
    [Required(ErrorMessage = ErrorMessageText.Required)]
    public string? ClientNumber { get; set; }
}
