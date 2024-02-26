using Application.Dto;

namespace Application.ResponseModels.QueriesResponseModels.ListValue
{
    public class GetListRowQueryResponse
    {
        public List<ListValueDto> ListRows { get; set; }
        
        public ListValueHeaders Headers { get; set; }
    }
}
