namespace ApiOS.Api;


public class Common
{
    public string GetAssemblyVersion()
    {
        return GetType().Assembly.GetName().Version.ToString();
    }
}

