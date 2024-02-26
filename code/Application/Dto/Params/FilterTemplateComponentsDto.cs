using Domain.Enums;


namespace Application.Dto.Params
{
    public class FilterTemplateComponentsDto : PaginatedRequestDto
    {
        public Int64? BulkProcessID { get; set; }
        public componentTypeEnum? DataType { get; set; }
    }
}
