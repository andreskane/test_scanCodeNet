using Application.ResponseModels.CommandResponseModels.BulkProcess;
using MediatR;

namespace Application.RequestModels.CommandRequestModels.BulkProcess
{
    public class AsignDynamicFormBulkProcessCommandRequest : IRequest<AsignDynamicFormBulkProcessCommandResponse>
    {
        public long BulkProcessId { get; set; }
        public List<long> DynamicFormsListId { get; set; }
    }
}