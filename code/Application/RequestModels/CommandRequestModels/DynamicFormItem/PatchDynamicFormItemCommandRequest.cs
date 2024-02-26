using Application.ResponseModels.CommandResponseModels.DynamicFormItem;
using Domain.Enums;
using MediatR;

namespace Application.RequestModels.CommandRequestModels.DynamicFormItem
{
    public class PatchDynamicFormItemCommandRequest : IRequest<PatchDynamicFormItemCommandResponse>
    {
        public Int64 DynamicFormItemId { get; set; }

        public DynamicFormStatusEnum Status { get; set; }
    }
}
