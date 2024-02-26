using System.ComponentModel;

namespace Domain.Enums;



public enum Action
{
    [Description("Creación")]
    Create = 0,
    [Description("Modificación")]
    Edit = 1,
    [Description("Eliminación")]
    Delete = 2
}
public enum RuleType
{
    ApiCall = 0,
    Script = 1,
    CallList = 2,
    CallListWithParams = 3

}
public enum DynamicFormStatusEnum
{
    Published = 1,
    UnPublished = 0
}
public enum ProcessTypeEnum
{

    NewElement = 1,
    EditElement = 2
}
public enum ProcessStatusEnum
{
    Draft = 0,
    OnWorking = 1,
    ReadyToProcess = 2,
    InProgress = 3,
    Completed = 4,
    Failed = 5

}
public enum DataType
{
    Integer,
    String,
    Date,
    Image

}
public enum componentTypeEnum
{
    date = 0,
    input = 1,
    number = 2,
    image = 3,
    dropdown = 4,
    input_textarea = 5,
    label = 6,
    title = 7,
    text = 8


}
public enum PlacementPreferenceEnum
{
    [Description("Previus Component")]
    PreviusComponent = 1,
    [Description("Following Component")]
    FollowingComponent = 2,
    [Description("At the beginning of the form")]
    StartForm = 3,
    [Description("At the end of the form")]
    EndForm = 4,
    LeaveOnSamePlace = 5
}
public enum ProcessState
{
    Pending, InProgress, ItDoneOk, ItDoneError, DoneOk, DoneError
}
public enum RequestTypeEnums
{
    Get,
    Post,
    Put,
    Delete
}
public enum RequestByEnums
{
    Body,
    Url,
    Form,
    Header
}

