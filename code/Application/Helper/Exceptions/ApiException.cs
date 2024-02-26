namespace Application.Helper.Exceptions;

public class ApiException : Exception
{
    public ApiException()
    { }

    public ApiException(string message)
        : base(message)
    {

        // message= message + Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    }

    public ApiException(string message, Exception innerException)
        : base(message, innerException)
    { }
}
