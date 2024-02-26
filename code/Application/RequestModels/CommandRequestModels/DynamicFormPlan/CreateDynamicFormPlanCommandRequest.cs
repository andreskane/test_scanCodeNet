using Application.ResponseModels.CommandResponseModels.DynamicFormPlan;
using MediatR;

namespace Application.RequestModels.CommandRequestModels.DynamicFormPlan
{
    public class CreateDynamicFormPlanCommandRequest : IRequest<CreateDynamicFormPlanCommandResponse>
    {
        public int DynamicFormId { get; set; }
        public int PlanId { get; set; }
    }
}
