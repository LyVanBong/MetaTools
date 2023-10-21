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
                "Mozilla/5.0 (Macintosh; Intel Mac OS X 14_0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Safari/537.36",
                "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Safari/537.36",
                "Mozilla/5.0 (iPhone; CPU iPhone OS 17_0 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) CriOS/118.0.5993.69 Mobile/15E148 Safari/604.1",
                "Mozilla/5.0 (iPod; CPU iPhone OS 17_0 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) CriOS/118.0.5993.69 Mobile/15E148 Safari/604.1",
                "Mozilla/5.0 (iPad; CPU OS 14_0 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) FxiOS/118.0 Mobile/15E148 Safari/605.1.15",
                "Mozilla/5.0 (Linux; Android 10) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.5993.65 Mobile Safari/537.36",
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