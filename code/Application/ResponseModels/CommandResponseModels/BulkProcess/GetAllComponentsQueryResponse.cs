using Application.Dto;

namespace Application.ResponseModels.CommandResponseModels.BulkProcess
{
    public class GetAllComponentsQueryResponse
    {
        public List<BulckComponentDto> ComponentList { get; set; }
    }
}
