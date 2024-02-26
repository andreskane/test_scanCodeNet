using Application.Dto;
using Application.ResponseModels.CommandResponseModels;
using MediatR;
using System.Runtime.Serialization;

namespace Application.RequestModels.CommandRequestModels
{
    public class UpdateDynamicFormTemplateCommandRequest : IRequest<UpdateDynamicFormTemplateCommandResponse>
    {
        [DataMember]
        public DynamicFormTemplateDto DynamicFormTemplate { get; set; }
    }
}
