namespace Domain.Entities.Layout;

public class DocLayoutTemplate
{
    public List<WorkflowItem> Pages { get; set; }
}
public class ItemTemplate
{
    public int ColSpanValue { get; set; }
    public string Component { get; set; }
    public bool Empty { get; set; }
    public string Icon { get; set; }
    public bool IsHidden { get; set; }
    public string Name { get; set; }
    public PropertiesTemplate Properties { get; set; }
    public bool Reordering { get; set; }
    public int RowSpanValue { get; set; }
    public string Type { get; set; }
    public string UniqueName { get; set; }
}
public class PropertiesTemplate
{
    public string Description { get; set; }
    public string InputId { get; set; }
    public bool IsHidden { get; set; }
    public string Label { get; set; }
    public bool Required { get; set; }
    public SelectedRule SelectedRule { get; set; }
    public string Value { get; set; }
}
