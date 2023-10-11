namespace MetaTools.Services.Facebook;

public class FacebookService : IFacebookService
{
    private readonly IRequestProvider _requestProvider;

    public FacebookService(IRequestProvider requestProvider)
    {
        _requestProvider = requestProvider;
    }

    public async Task<(string action, string lsd, string jazoest, string m_ts, string li, string login, string bi_xrwh, string unrecognized_tries, string try_number)> GetParaLogin(string ua)
    {
        try
        {
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("User-Agent",ua),
            };
            string html = await _requestProvider.GetAsync("https://d.facebook.com/login.php", headers: headers);

            html = Regex.Match(html, @"<form(.*?)</form>")?.Groups[1]?.Value;

            string action = Regex.Match(html, @"action=""(.*?)""")?.Groups[1]?.Value;
            action = HttpUtility.HtmlDecode(action);

            string lsd = Regex.Match(html, @"name=""lsd"" value=""(.*?)""")?.Groups[1]?.Value;

            string jazoest = Regex.Match(html, @"name=""jazoest"" value=""(.*?)""")?.Groups[1]?.Value;

            string m_ts = Regex.Match(html, @"name=""m_ts"" value=""(.*?)""")?.Groups[1]?.Value;

            string li = Regex.Match(html, @"name=""li"" value=""(.*?)""")?.Groups[1]?.Value;

            string bi_xrwh = Regex.Match(html, @"name=""bi_xrwh"" value=""(.*?)""")?.Groups[1]?.Value;

            string login = Regex.Match(html, @"<input value=""(.*?)"" type=""submit"" name=""login""")?.Groups[1]
                ?.Value;
            login = HttpUtility.HtmlDecode(login);

            string unrecognized_tries = Regex.Match(html, @"name=""unrecognized_tries"" value=""(.*?)""")?.Groups[1]?.Value;

            string try_number = Regex.Match(html, @"name=""try_number"" value=""(.*?)""")?.Groups[1]?.Value;


            return (action: action, lsd: lsd, jazoest: jazoest, m_ts: m_ts, li: li, bi_xrwh: bi_xrwh, login: login, unrecognized_tries: unrecognized_tries, try_number: try_number);
        }
        catch (Exception e)
        {
            Crashes.TrackError(e);
        }

        return default;
    }

    public async Task<string> Login(string action, string email, string pass, string ua, string lsd, string jazoest, string m_ts, string li, string try_number, string unrecognized_tries, string login, string bi_xrwh)
    {
        try
        {
            var header = new List<KeyValuePair<string, string>>();
            header.Add(new("User-Agent", ua));
            header.Add(new("origin", "https://d.facebook.com"));
            header.Add(new("referer", "https://d.facebook.com/login.php"));
            header.Add(new("viewport-width", Random.Shared.Next(500, 1200) + ""));
            var collection = new List<KeyValuePair<string, string>>();
            collection.Add(new("lsd", lsd));
            collection.Add(new("jazoest", jazoest));
            collection.Add(new("m_ts", m_ts));
            collection.Add(new("li", li));
            collection.Add(new("try_number", try_number));
            collection.Add(new("unrecognized_tries", unrecognized_tries));
            collection.Add(new("email", email));
            collection.Add(new("pass", pass));
            collection.Add(new("login", login));
            collection.Add(new("bi_xrwh", bi_xrwh));

            var data = await _requestProvider.GetCookieAsync("https://d.facebook.com" + action, method: HttpMethod.Post, header, collection);
            var cookies = data.Cookie.GetCookies(new Uri("https://d.facebook.com" + action));
            foreach (Cookie cookie in cookies)
            {

            }

        }
        catch (Exception e)
        {
            Crashes.TrackError(e);
        }

        return string.Empty;
    }
}