namespace Infrastructure.Rest;



public abstract class BaseRestClient
{
    protected string _uri_KeyName;
    private string _urlService;

    private int _siteId;
    private int _userId;

    private ConnectureOS.Framework.Net.RestClient.Client _restClient;
    private Dictionary<string, object> _headers;



    public BaseRestClient(int siteId, int userId, string urlService)
    {
        this.Init(siteId, userId);
        _siteId = siteId;
        _userId = userId;
        _urlService = urlService;// ConfigurationManager.AppSettings[this._uri_KeyName].ToString();
        SetHeaders();

        _restClient = new ConnectureOS.Framework.Net.RestClient.Client(_urlService, _headers);
    }
    protected virtual void SetHeaders()
    {
        _headers = new Dictionary<string, object>();
        AddHeader("id_site", GetSiteId());
        AddHeader("app", "ConnectureOS");
        AddHeader("user_id", GetUser());
        AddHeader("token", "thisissparta");
    }

    protected void AddHeader(string key, object value)
    {
        _headers.Add(key, value);
    }

    protected int GetSiteId()
    {
        return _siteId;
    }
    protected string SetToken(string Token)
    {

        return string.Format("Bearer {0}", Token);
    }

    protected Int32 GetUser()
    {
        return _userId;
    }
    protected abstract void Init(int siteId, int userId);




}