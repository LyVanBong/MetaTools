namespace MetaTools.Services.UserAgent;

public class UserAgentService : IUserAgentService
{
    private readonly IRequestProvider _requestProvider;

    public UserAgentService(IRequestProvider requestProvider)
    {
        _requestProvider = requestProvider;
    }

    public string Generate()
    {
        try
        {
            // https://www.useragents.me/#most-common-mobile-useragents-json-csv
            string[] ua = new[]
            {
               "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Safari/537.36",
               "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Safari/537.36",
               "Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Safari/537.36",
               "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Safari/537.36 Edg/118.0.2088.57",
               "Mozilla/5.0 (Windows NT 10.0; Win64; x64; Xbox; Xbox One) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Safari/537.36 Edge/44.18363.8131",
               "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/118.0",
               "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:115.0) Gecko/20100101 Firefox/115.0",
               "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Safari/537.36 OPR/103.0.0.0",
               "Mozilla/5.0 (Windows NT 10.0; WOW64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Safari/537.36 OPR/103.0.0.0"
            };

            return ua[Random.Shared.Next(ua.Length)];
        }
        catch (Exception e)
        {
            Crashes.TrackError(e);
            return null;
        }
    }

    public Dictionary<string, string> FakeBrowserHeadersApi(string ua, Dictionary<string, string> dic = null)
    {
        try
        {
            var data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>[]>>(ua);

            var header = data["result"][0];

            if (dic != null)
            {
                foreach (var pair in dic)
                {
                    header.Add(pair.Key, pair.Value);
                }
            }

            header.Add("sec-ch-prefers-color-scheme", "dark");

            header.Add("viewport-width", Random.Shared.Next(500, 1800) + "");

            return header;
        }
        catch (Exception e)
        {
            Crashes.TrackError(e);
        }

        return null;
    }
}