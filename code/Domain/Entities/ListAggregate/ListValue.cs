using Domain.Entities.Common;
using Newtonsoft.Json;

namespace Domain.Entities.ListAggregate
{
    public class ListValue : BaseAuditableEntity
    {
        public Int64 ListId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        [JsonIgnore]
        public ListDefinition ListDefinition { get; set; }
    }
}
