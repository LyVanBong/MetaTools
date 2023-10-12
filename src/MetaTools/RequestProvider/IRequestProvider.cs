namespace MetaTools.RequestProvider;

public interface IRequestProvider
{
    Task<string> GetAsync(string url, List<KeyValuePair<string, string>> headers = null, List<KeyValuePair<string, string>> body = null, string proxy = null);

    Task<string> PostAsync(string url, List<KeyValuePair<string, string>> headers = null, List<KeyValuePair<string, string>> body = null, string proxy = null);

    Task<string> PutAsync(string url, List<KeyValuePair<string, string>> headers = null, List<KeyValuePair<string, string>> body = null, string proxy = null);

    Task<string> PatchAsync(string url, List<KeyValuePair<string, string>> headers = null, List<KeyValuePair<string, string>> body = null, string proxy = null);

    Task<string> DeleteAsync(string url, List<KeyValuePair<string, string>> headers = null, List<KeyValuePair<string, string>> body = null, string proxy = null);
    Task<(string Content, CookieContainer Cookie)> GetCookieAsync(string url,
        HttpMethod method,
        List<KeyValuePair<string, string>> headers = null,
        List<KeyValuePair<string, string>> body = null,
        string proxy = null,
        CookieContainer cookieContainer = null
        );
}