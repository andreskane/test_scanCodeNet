using Application.Dto.Enums;

namespace Application.Dto
{
    public class DynamicFormProductAttributeDto
    {
        public Int64 Id { get; set; }
        public string ZipCode { get; set; }
        public long ProductId { get; set; }
        public long DynamicFormId { get; set; }
        public string AtributteCode { get; set; }
        public DataTypeDto DataType { get; set; }
        public string DefaultValue { get; set; }
        public string ExtraInfo { get; set; }
        public string Metadata { get; set; }
        public bool Optional { get; set; } 
    }
}
