using ConnectureOS.Framework.Message;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Application.Dto.Params;

public class FilterPersonByRutDto : PaginatedRequestDto
{
    [DataMember]
    [Required(ErrorMessage = ErrorMessageText.Required)]
    public string Rut { get; set; }
}
