using Application.ResponseModels.CommandResponseModels.BulkProcess;
using Domain.Enums;
using MediatR;

namespace Application.RequestModels.CommandRequestModels.BulkProcess
{
    public class CreateBulkProcessCommandRequest : IRequest<CreateBulkProcessCommandResponse>
    {


        
        public string Name { get; set; }
        public ProcessTypeEnum ProcessType { get; set; }
        public ProcessStatusEnum Status { get; set; }
      


    }
}