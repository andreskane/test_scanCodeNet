using Microsoft.Azure.KeyVault.Models;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Configuration;

namespace Application.Infrastructure;

public class AppSettingsHelper
{
    #region Private
    /// <summary>
    /// Key vault name
    /// </summary>
    private static string KeyVaultName => Get<string>("KeyVaultName", null, true);
    /// <summary>
    /// If it's true, gets values from web.config otherwise from Azure Key Vault.
    /// </summary>
    private static bool IsSaaS => Get<bool>("IsSaaS", true, true);
    #endregion Private

    #region Cache
    private static readonly ConcurrentDictionary<String, Object> configurationCache = new ConcurrentDictionary<String, Object>();
    #endregion Cache

    #region GetSecret
    /// <summary>
    /// Get secret value from Azure Key Vault
    /// </summary>
    private static SecretBundle GetSecret(string secretName, string vaultName = null)
    {


        Task<SecretBundle> secret = null;

        return secret.Result;
    }
    #endregion GetSecret

    #region Get
    
    public static T Get<T>(string key, T defaultValue, bool throwExecption = false)
    {
        T value = defaultValue;

        string appSetting = ConfigurationManager.AppSettings[key];

        if (String.IsNullOrWhiteSpace(appSetting) && throwExecption)
            throw new AppSettingNotFoundException(key);
        else if (appSetting != null)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                value = (T)(converter.ConvertFromInvariantString(appSetting));
            }
            catch (Exception ex)
            {
                if (throwExecption)
                    throw new AppSettingException(String.Format("Error converting AppSetting:{0}", key), ex);
            }
        }
        return value;
    }
    #endregion Get

    #region GetAppSettingValue
    /// <summary>
    /// Gets value from AzureKeyVault, or from web.config if IsDevelop is true.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="secretName">secret or app key.</param>
    /// <param name="defaultValue">default value</param>
    /// <param name="throwExecption">throws an exception if secretName if not defined</param>
    /// <returns></returns>
    public static T GetAppSettingValue<T>(string secretName, T defaultValue = default(T), bool throwExecption = false)
    {
        string key = $"{KeyVaultName}:{secretName}";

        Object valueFactory(String _)
        {
            if (!IsSaaS)
                return Get(secretName, defaultValue, throwExecption);
            else
            {
                var secret = GetSecret(secretName);

                var converter = TypeDescriptor.GetConverter(typeof(T));
                return (converter.ConvertFromInvariantString(secret.Value));
            }
        }

        return (T)configurationCache.GetOrAdd(key, valueFactory);
    }
    #endregion GetAppSettingValue

    #region GetConnectionStringValue
    /// <summary>
    /// Gets connectionString from Azure Key Vault or from web.config if IsDevelop is true.
    /// </summary>
    /// <param name="secretName"></param>
    /// <returns></returns>
    public static String GetConnectionStringValue(String secretName)
    {
        String vaultName = KeyVaultName;
        String key = $"{vaultName}:{secretName}";

        Object valueFactory(String _)
        {
            if (!IsSaaS)
                return ConfigurationManager.ConnectionStrings[secretName].ConnectionString;
            else
            {
                SecretBundle secret = GetSecret(secretName);
                return secret.Value;
            }
        }

        return configurationCache.GetOrAdd(key, valueFactory) as String;
    }
    #endregion GetConnectionStringValue
}
