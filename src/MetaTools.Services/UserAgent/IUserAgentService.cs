namespace MetaTools.Services.UserAgent;

public interface IUserAgentService
{
    string Generate();

    Dictionary<string, string> FakeBrowserHeadersApi(string ua, Dictionary<string, string> dic = null);
}