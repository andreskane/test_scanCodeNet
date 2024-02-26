using Application.ResponseModels.CommandResponseModels.BulkProcess;
using MediatR;

namespace Application.RequestModels.CommandRequestModels.BulkProcess
{
    public class GetAllComponentQueryRequest : IRequest<GetAllComponentsQueryResponse>
    {
        public Int64 BulkProcessId { get; set; }
    }
}
