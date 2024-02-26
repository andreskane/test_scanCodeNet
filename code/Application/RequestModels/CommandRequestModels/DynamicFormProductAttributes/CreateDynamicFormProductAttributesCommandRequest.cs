using Application.Dto;
using Application.ResponseModels.CommandResponseModels;
using MediatR;
namespace Application.RequestModels.CommandRequestModels
{
    public class CreateDynamicFormProductAttributesCommandRequest : IRequest<CreateDynamicFormProductAttributesCommandResponse>
    {
        public DynamicFormProductAttributeDto WProductAttribute { get; set; }
    }
}
