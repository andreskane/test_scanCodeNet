namespace Application.Helper.Exceptions;

public class FakeException : Exception
{
    public int? Code { get; set; }

    public FakeException(int code, string message)
      : base(message)
    {
        Code = code;
    }
    public FakeException(string message)
        : base(message)
    {

        // message= message + Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    }

    public FakeException(string message, Exception innerException)
        : base(message, innerException)
    { }
   
}
