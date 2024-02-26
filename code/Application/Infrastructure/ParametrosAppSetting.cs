using System.Configuration;

namespace Application.Infrastructure;

public static class ParametrosAppSetting
{

    #region Envio de mails
    public static bool MailSettingsEnableSsl
    {
        get { return bool.Parse(GetSettingDefault("MailsSettings:EnableSsl", "False")); }
    }
    #endregion
    #region Cache
    public static int CacheExpirationMinutes
    {
        get { return int.Parse(GetSettingDefault("CONFIG:CacheExpirationMinutes", "30")); }
    }
    #endregion
    #region Valores Rutas Archivos
    public static string TxtEmailsPath
    {
        get { return GetSettingDefault("CONFIG:TxtEmailsPath", "TxtEmailsPath"); }
    }
    public static string XmlConfiguracionPath
    {
        get { return GetSettingDefault("CONFIG:XmlConfiguracionPath", "XmlConfiguracionPath"); }
    }
    public static string XmlVentaGrabacionPath
    {
        get { return GetSettingDefault("CONFIG:XmlVentaGrabacionPath", @"C:\RootSitesApiAON\XMLVenta\"); }
    }
    public static string XmlEndosoGrabacionPath
    {
        get { return GetSettingDefault("CONFIG:XmlEndosoGrabacionPath", @"C:\RootSitesApiAON\XMLEndoso\"); }
    }
    #endregion

    #region Valores Azure

    public static int AzureMinutesToLiveSAS
    {
        get { return int.Parse(GetSettingDefault("CONFIG:AzureMinutesToLiveSAS", "30")); }
    }
    #endregion
    #region Helpers
    private static string GetSettingDefault(string codSetting, string defaultValue)
    {
        string retorno = ConfigurationManager.AppSettings[codSetting];
        if (string.IsNullOrWhiteSpace(retorno))
            return defaultValue;
        return retorno;
    }
    #endregion
}
