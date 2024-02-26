using Application.Dto;
using Application.ResponseModels.CommandResponseModels.ListValue;
using MediatR;

namespace Application.RequestModels.CommandRequestModels.ListValue
{
    public class UpdateListValueCommandRequest : IRequest<UpdateListValueCommandResponse>
    {
        public List<ListValueDto> ListValues { get; set; }
        public Int64 ListId { get; set; }
    }
}
