namespace Application.Dto.Params.ListValue
{
    public class ListValueFilter : PaginatedRequestDto
    {
        public Int64 ListId { get; set; }
        public Int64? DynamicFormId { get; set; }
        public string? TenantId { get; set; }
    }
}
