namespace Application.Dto;

public class RootObjectDto
{
    public List<DocDynamicFormDto> Pages { get; set; }


}


public class DocDynamicFormDto
{
    public int page { get; set; }
   
    public string pageName { get; set; }
    public string defaultPageName { get; set; }
    public Boolean isTemplateActive { get; set; }
    public string pageButtonRule { get; set; }
    public List<WorkflowItemDto> workflowTable { get; set; }
}
public class WorkflowItemDto
{
    public bool reordering { get; set; } 
    public bool empty { get; set; } 
    public string name { get; set; } 
    public string type { get; set; } 
    public string icon { get; set; } 
    public string uniqueName { get; set; } 
    public string component { get; set; } 
    public int colSpanValue { get; set; } 
    public int rowSpanValue { get; set; } 
    public bool isHidden { get; set; } 
    public Int64 templateId { get; set; } 
    public PropertiesBaseDto? properties { get; set; } 

    public WorkflowItemDto()
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



        properties = new PropertiesBaseDto() { selectedRule = new SelectedRuleDto() };

        properties = new InputPropertiesDto() { selectedRule = new SelectedRuleDto() };

    }
}

public class SelectedRuleDto
{
    public string id { get; set; } 
    public string value { get; set; }
    public string description { get; set; }

    public SelectedRuleDto()
    {
        id = "";
        value = "";
        description = "";

    }
}



public class PropertiesBaseDto
{
    public string description { get; set; }
    public string inputId { get; set; }
    public string value { get; set; }
    public string label { get; set; }
    public string color { get; set; }
    public string fontSize { get; set; }
    public string fontWeight { get; set; }
    public bool isHidden { get; set; }
    public bool required { get; set; }
    public bool disabled { get; set; }
    public SelectedRuleDto selectedRule { get; set; }

}

public class TextAreaPropertiesDto : PropertiesBaseDto
{
    public String placeholder { get; set; }

     
    public int minRows { get; set; }
    public int maxRows { get; set; }
}
public class DropDownPropertiesDto : PropertiesBaseDto
{
    public Int64? selectedListId { get; set; }

    public List<OptionDto> options { get; set; }
    public OptionDto selectedOption { get; set; }
}
public class InputPropertiesDto : PropertiesBaseDto
{  
    public string label { get; set; }
    public string placeholder { get; set; }
    public bool required { get; set; }
    public bool disabled { get; set; }
}
public class ImagePropertiesDto : PropertiesBaseDto
{  
    public string src { get; set; }
    public string alt { get; set; }
    public string maxWidth { get; set; }
    public string maxHeight { get; set; }
    public ImagePropertiesDto()
    {
        description = "";
        inputId = "";
        value = "";
        color = "";
        fontSize = "";
        fontWeight = "";
        isHidden = false;
        selectedRule = new SelectedRuleDto();
    }
}

public class DatePropertiesDto : PropertiesBaseDto
{
}


public class TitlePropertiesDto : PropertiesBaseDto
{
}
public class TetxPropertiesDto : PropertiesBaseDto
{
}
public class NumberPropertiesDto : PropertiesBaseDto
{
}
public class LabelPropertiesDto : PropertiesBaseDto
{
}



public class OptionDto
{
    public string value { get; set; }
    public string viewValue { get; set; }
}

public class SelectedOptionDto
{
    public string id { get; set; }
    public string value { get; set; }
}

  