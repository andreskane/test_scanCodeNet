using System.Text.Json;

namespace Application.Helper.Filters;

public class ErrorDetails
{

    public ErrorDetails(string source, string message)
    {
        Source = source;
        Message = message;
    }

    public string Source { get; set; }

    public string Message { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this);

}
