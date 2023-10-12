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
            var cookies = data.Cookie;

            var para = await GetParaLogin(ua);

            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.UseCookies = true;
            httpClientHandler.CookieContainer = cookies;

            var ck2 = cookies.GetAllCookies();
            foreach (Cookie o in ck2)
            {
                
            }

            var client = new HttpClient(httpClientHandler);
            var request = new HttpRequestMessage(HttpMethod.Post, "https://d.facebook.com/login/checkpoint/");
            request.Headers.Add("authority", "d.facebook.com");
            request.Headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
            request.Headers.Add("accept-language", "en-US,en;q=0.9");
            request.Headers.Add("cache-control", "max-age=0");
            request.Headers.Add("dpr", "1");
            request.Headers.Add("origin", "https://d.facebook.com");
            request.Headers.Add("sec-ch-prefers-color-scheme", "dark");
            request.Headers.Add("sec-fetch-dest", "document");
            request.Headers.Add("sec-fetch-mode", "navigate");
            request.Headers.Add("sec-fetch-site", "same-origin");
            request.Headers.Add("sec-fetch-user", "?1");
            request.Headers.Add("upgrade-insecure-requests", "1");
            request.Headers.Add("user-agent", ua);
            request.Headers.Add("viewport-width", "709");
            collection = new List<KeyValuePair<string, string>>();
            collection.Add(new("fb_dtsg", "V87tjjmjz7Y="));
            collection.Add(new("jazoest", "21067"));
            collection.Add(new("checkpoint_data", ""));
            collection.Add(new("approvals_code", "654321"));
            collection.Add(new("codes_submitted", "0"));
            collection.Add(new("submit[Submit Code]", "Submit Code"));
            collection.Add(new("nh", "a2d4e73a12af73bff1325cd14a25520bc8aecd05"));
            var content = new FormUrlEncodedContent(collection);
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var data2 = await response.Content.ReadAsStringAsync();



        }
        catch (Exception e)
        {
            Crashes.TrackError(e);
        }

        return string.Empty;
    }
}