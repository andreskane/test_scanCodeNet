namespace Domain.Entities.Layout;

public class RootObject
{
    public List<DocDynamicForm> Pages { get; set; }
}

public class DocDynamicForm//OK
{
    public int page { get; set; } 
    public string pageName { get; set; }
    public string defaultPageName { get; set; }
    public Boolean isTemplateActive { get; set; }
    public string pageButtonRule { get; set; }
    public List<WorkflowItem> workflowTable { get; set; }
}
public class WorkflowItem//OK
{
    public bool reordering { get; set; }//OK
    public bool empty { get; set; }//OK
    public string name { get; set; }//OK
    public string type { get; set; }//OK
    public string icon { get; set; }//OK
    public string uniqueName { get; set; }//OK
    public string component { get; set; }//OK
    public int colSpanValue { get; set; }//OK
    public int rowSpanValue { get; set; }//OK
    public bool isHidden { get; set; }//OK
    public Int64 templateId { get; set; }//OK
    public Properties? properties { get; set; }//OK

    public WorkflowItem()
    {

        reordering = false;
        empty = true;
        name = "";
        type = "";
        icon = "";
        uniqueName = "";
        component = "";
        colSpanValue = 0;
        rowSpanValue = 0;
        isHidden = false;



        properties = new Properties() { selectedRule = new SelectedRule() };
    }
}
public class Option
{
    public string value { get; set; }
    public string viewValue { get; set; }
}
public class Properties
{
    public string description { get; set; }
    public string inputId { get; set; }
    public string label { get; set; }
    public string value { get; set; }
    public bool isHidden { get; set; }
    public bool required { get; set; }
    public Int64? selectedListId { get; set; }
    public string src { get; set; }
    public string alt { get; set; }
    public string maxHeight { get; set; }
    public string maxWidth { get; set; }
    public string color { get; set; }
    public string fontSize { get; set; }
    public string fontWeight { get; set; }
    public string placeholder { get; set; }
    public bool disabled { get; set; }

    public int minRows { get; set; }
    public int maxRows { get; set; }
    public Option? selectedOption { get; set; }
    public List<Option>? options { get; set; }

    public SelectedRule? selectedRule { get; set; }

    public Properties()
    {
        description = "";
        inputId = "";
        label = "";
        value = "";
        isHidden = false;
        required = false;
        src = "";
        alt = "";
        maxHeight = "";
        maxWidth = "";
        color = "";
        fontSize = "";
        fontWeight = "";
        placeholder = "";
        disabled = false;
        selectedRule = new SelectedRule();
        options = new List<Option>();
        selectedOption = new Option();

    }
}
public class SelectedRule
{
    public string id { get; set; }//todo:check this
    public string value { get; set; }
    public string description { get; set; }

    public SelectedRule()
    {
        id = "";
        value = "";
        description = "";

    }
}