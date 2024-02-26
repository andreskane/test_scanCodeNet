using Application.ResponseModels.QueriesResponseModels;
using ConnectureOS.Framework.Message;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Application.RequestModels.QueriesRequestModels;


public class GetFullPersonByRutQuery : IRequest<GetFullPersonResponse>
{
    [DataMember]
    [Required(ErrorMessage = ErrorMessageText.Required)]
    public string Rut { get; set; }
}
