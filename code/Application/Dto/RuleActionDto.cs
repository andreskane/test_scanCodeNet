using Domain.Enums;

namespace Application.Dto
{
    public class RuleActionDto
    {
        public Int64? Id { get; set; }
        public RuleType ActionType { get; set; }
        public RequestTypeEnums? RequestType { get; set; }
        public RequestByEnums? RequestBy { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public bool IsActive { get; set; }
        public string Url { get; set; }
        public string WhenScript { get; set; }
        public string ThenScript { get; set; }
        public int Order { get; set; }
        public Int64? RuleId { get; set; }
        public IList<ActionParameterDto> Parameters { get; set; }
    }
}
