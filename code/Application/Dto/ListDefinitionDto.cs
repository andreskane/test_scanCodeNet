namespace Application.Dto
{
    public class ListDefinitionDto
    {
        public Int64 Id { get; set; }
        public string ListName { get; set; }
        public string Description { get; set; }
        public Int64? DynamicFormId { get; set; }
        public string TenantId { get; set; }
        public string KeyName { get; set; }
        public string ValueName { get; set; }
        public string DataType { get; set; }
    }
}
