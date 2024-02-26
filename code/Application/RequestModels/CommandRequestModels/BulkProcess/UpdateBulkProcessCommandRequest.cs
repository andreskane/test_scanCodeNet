using Application.Dto;
using Application.ResponseModels.CommandResponseModels.BulkProcess;
using MediatR;
using System.Runtime.Serialization;

namespace Application.RequestModels.CommandRequestModels.BulkProcess
{
    public class UpdateBulkProcessCommandRequest : IRequest<UpdateBulkProcessCommandResponse>
    {
        [DataMember]
        public BulkProcessRequestDto bulkProcess { get; set; }
        

    }
}