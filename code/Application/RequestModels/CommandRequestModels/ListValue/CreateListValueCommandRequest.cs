using Application.Dto;
using Application.ResponseModels.CommandResponseModels.ListValue;
using MediatR;

namespace Application.RequestModels.CommandRequestModels.ListValue
{
    public class CreateListValueCommandRequest : IRequest<CreateListValueCommandResponse>
    {
        public List<ListValueDto> ListValues { get; set; }
    }
}
