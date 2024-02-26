using Application.ResponseModels.CommandResponseModels.BulkProcess;
using Domain.Entities;
using MediatR;

namespace Application.RequestModels.CommandRequestModels.BulkProcess
{
    public class AddComponentsBulkProcessCommandRequest : IRequest<AddComponentsBulkProcessCommandResponse>
    {
        public long BulkProcessId { get; set; }
        public List<BulckComponent> Components { get; set; }
    }
}