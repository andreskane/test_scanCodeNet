using Application.ResponseModels.QueriesResponseModels;
using ConnectureOS.Framework.Message;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Application.RequestModels.QueriesRequestModels;

public class GetObfuscatedContactDataQuery : IRequest<GetObfuscatedContactDataResponse>
{
    [DataMember]
    [Required(ErrorMessage = ErrorMessageText.Required)]
    public string Rut { get; set; }
}
