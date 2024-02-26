namespace Application.Dto
{
    public class ListRowDto
    {
        public Int64 ListId { get; set; }
        public string ListName { get; set; }
        public Int64? DynamicFormId { get; set; }
        public string TenantId { get; set; }
        public string KeyName { get; set; }
        public string ValueName { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
