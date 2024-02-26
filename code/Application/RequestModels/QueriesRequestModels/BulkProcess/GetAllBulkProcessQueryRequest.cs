using Application.Dto.Params.BulkProcess;

namespace ApiOS.Controllers
{
    public class GetAllBulkProcessQueryRequest : MediatR.IRequest<GetAllBulkProcessQueryResponse>
    {
        public FilterBulkProcess Filter { get; set; }
    }
}