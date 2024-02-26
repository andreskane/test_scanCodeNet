using Application.ResponseModels.CommandResponseModels.DynamicFormItem;
using MediatR;

namespace Application.RequestModels.CommandRequestModels.DynamicFormItem
{
    public class CopyDynamicFormItemCommandRequest : IRequest<CopyDynamicFormItemCommandResponse>
    {
        public Int64 DynamicFormItemId { get; set; }
    }
}
