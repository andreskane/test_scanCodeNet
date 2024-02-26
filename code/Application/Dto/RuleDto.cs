namespace Application.Dto
{
    public class RuleDto
    {
        public Int64? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public bool Enabled { get; set; }
        public string? KeyDocument { get; set; }
        public Int64? DinamicFormsCount { get; set; }
        public IList<RuleActionDto> Actions { get; set; }

        public IList<Int64>? DynamicFormListID { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }

    }
}
