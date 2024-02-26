using Application.ResponseModels.QueriesResponseModels;
using ConnectureOS.Framework.Message;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Application.RequestModels.QueriesRequestModels;


public class GetPersonByRutQuery : IRequest<GetPersonByRutResponse>
{
    [DataMember]
    [Required(ErrorMessage = ErrorMessageText.Required)]
    public string Rut { get; set; }
    public int? sLdap { get; set; }
}
