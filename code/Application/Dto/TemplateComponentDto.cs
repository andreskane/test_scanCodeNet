namespace Application.Dto
{
    public class TemplateComponentDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string DataType { get; set; }
        public string? Label { get; set; }
        public string? value { get; set; }
        public string? data { get; set; }
        public string? placeholder { get; set; }
        public string? groupID { get; set; }
        public Boolean IsRequired { get; set; }
        public Boolean IsHidden { get; set; }
    }
}
