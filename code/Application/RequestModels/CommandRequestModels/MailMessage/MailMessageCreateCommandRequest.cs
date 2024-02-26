using Application.Dto;
using MediatR;

namespace Application.RequestModels.CommandRequestModels;

public class MailMessageCreateCommandRequest : IRequest
{
    public CustomerDto mensaje { get; set; }
    public Int32 EnvioID { get; set; }
    public String Campania { get; set; }
    public DateTime? fecha_para_envio { get; set; }
}
