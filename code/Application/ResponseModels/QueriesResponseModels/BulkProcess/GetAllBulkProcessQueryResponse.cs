using Application.Dto;
using Application.RequestModels.Extensions;

namespace ApiOS.Controllers
{
    public class GetAllBulkProcessQueryResponse
    {
        public PaginatedList<BulkProcessDto> bulkProcessList { get; set; }
    }
}