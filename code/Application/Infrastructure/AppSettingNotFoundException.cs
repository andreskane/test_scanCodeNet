namespace Application.Infrastructure;

public class AppSettingNotFoundException : Exception
{
    public AppSettingNotFoundException(string key) : base(String.Format("No se encontro AppSetting:{0}", key))
    {
    }
}
public class AppSettingException : Exception
{
    public AppSettingException(string message) : base(message)
    {
    }

    public AppSettingException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
