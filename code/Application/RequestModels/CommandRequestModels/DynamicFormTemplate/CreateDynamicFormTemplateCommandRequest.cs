using Application.ResponseModels.CommandResponseModels;
using Domain.Enums;
using MediatR;
using System.Runtime.Serialization;

namespace Application.RequestModels.CommandRequestModels
{
    public class CreateDynamicFormTemplateCommandRequest : IRequest<CreateDynamicFormTemplateCommandResponse>
    {
        [DataMember]


        public string Name { get; set; }
        [DataMember]

        public string Description { get; set; }
        [DataMember]

        public DynamicFormStatusEnum State { get; set; }

       

        [DataMember]
        public string Layout { get; set; }
        
    }
}
