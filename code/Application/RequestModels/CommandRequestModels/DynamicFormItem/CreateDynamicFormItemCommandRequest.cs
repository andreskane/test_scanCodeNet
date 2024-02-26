using Application.Dto;
using Application.ResponseModels.CommandResponseModels;
using MediatR;

namespace Application.RequestModels.CommandRequestModels
{
    public class CreateDynamicFormItemCommandRequest : IRequest<CreateDynamicFormItemCommandResponse>
    {
        public DynamicFormItemDto WDynamicForm { get; set; }
    }
}
