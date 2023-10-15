using Leaf.xNet;
using MetaTools.Services.TwoFactorAuthentication;

namespace MetaTools.Services.Facebook;

public class FacebookService : IFacebookService
{
    private readonly ITwoFactorAuthentication _twoFactorAuthentication;
    private readonly IUserAgentService _userAgentService;

    public FacebookService(ITwoFactorAuthentication twoFactorAuthentication, IUserAgentService userAgentService)
    {
        _twoFactorAuthentication = twoFactorAuthentication;
        _userAgentService = userAgentService;
    }

    public string GetAccessTokenEaab(string cookie, string ua)
    {
        try
        {
            using (HttpRequest request = new HttpRequest())
            {
                request.AddHeader("authority", "adsmanager.facebook.com");
                request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                request.AddHeader("accept-language", "en-US,en;q=0.9");
                request.AddHeader("cookie", cookie);
                request.AddHeader("User-Agent", ua);
                request.AddHeader("dpr", "1");
                request.AddHeader("sec-ch-prefers-color-scheme", "dark");
                request.AddHeader("sec-ch-ua-mobile", "?0");
                request.AddHeader("sec-ch-ua-model", "\"\"");
                request.AddHeader("sec-fetch-dest", "document");
                request.AddHeader("sec-fetch-mode", "navigate");
                request.AddHeader("sec-fetch-site", "same-origin");
                request.AddHeader("upgrade-insecure-requests", "1");
                request.AddHeader("viewport-width", Random.Shared.Next(500, 1800) + "");
                var respone = request.Get("https://www.facebook.com/adsmanager/");

                string html = respone.ToString();

                if (html.Contains("window.location.replace"))
                {
                    string url = Regex.Match(html, @"window.location.replace\(""(.*?)""").Groups[1].Value;

                    url = url.Replace("\\", "");

                    request.AddHeader("authority", "adsmanager.facebook.com");
                    request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                    request.AddHeader("accept-language", "en-US,en;q=0.9");
                    request.AddHeader("cookie", cookie);
                    request.AddHeader("User-Agent", ua);
                    request.AddHeader("dpr", "1");
                    request.AddHeader("sec-ch-prefers-color-scheme", "dark");
                    request.AddHeader("sec-ch-ua-mobile", "?0");
                    request.AddHeader("sec-ch-ua-model", "\"\"");
                    request.AddHeader("sec-fetch-dest", "document");
                    request.AddHeader("sec-fetch-mode", "navigate");
                    request.AddHeader("sec-fetch-site", "same-origin");
                    request.AddHeader("upgrade-insecure-requests", "1");
                    request.AddHeader("viewport-width", Random.Shared.Next(500, 1800) + "");

                    respone = request.Get(url);

                    html = respone.ToString();

                    if (html.Contains("EAA"))
                    {
                        return Regex.Match(html, @"window.__accessToken=""(.*?)""").Groups[1].Value;
                    }
                }
            }

        }
        catch (Exception e)
        {
            Crashes.TrackError(e);
        }

        return string.Empty;
    }

    public string Login(string email, string pass, string secretKey, string ua, string proxy)
    {
        try
        {
            using (HttpRequest request = new HttpRequest())
            {

                if (proxy != null)
                    request.Proxy = HttpProxyClient.Parse(proxy);

                request.AddHeader("authority", "d.facebook.com");
                request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                request.AddHeader("accept-language", "en-US,en;q=0.9");
                request.AddHeader("sec-ch-ua-mobile", "?0");
                request.AddHeader("sec-fetch-dest", "document");
                request.AddHeader("sec-fetch-mode", "navigate");
                request.AddHeader("sec-fetch-site", "none");
                request.AddHeader("sec-fetch-user", "?1");
                request.AddHeader("upgrade-insecure-requests", "1");
                request.AddHeader("user-agent", ua);

                var respone = request.Get("https://d.facebook.com/login.php");

                string html = respone.ToString();

                string cookie = respone.Cookies.GetCookieHeader(respone.Address);

                if (cookie.Contains("c_user"))
                {
                    return cookie;
                }

                var match = Regex.Match(html, @"<form(.*?)</form>");

                html = match.Value;

                if (string.IsNullOrEmpty(html)) return string.Empty;

                string action = HttpUtility.HtmlDecode(Regex.Match(html, @"action=""(.*?)""").Groups[1].Value);

                string jazoest = Regex.Match(html, @"name=""jazoest"" value=""(.*?)""").Groups[1].Value;

                string lsd = Regex.Match(html, @"name=""lsd"" value=""(.*?)""").Groups[1].Value;

                string m_ts = Regex.Match(html, @"name=""m_ts"" value=""(.*?)""").Groups[1].Value;

                string li = Regex.Match(html, @"name=""li"" value=""(.*?)""").Groups[1].Value;

                string try_number = Regex.Match(html, @"name=""try_number"" value=""(.*?)""").Groups[1].Value;

                string unrecognized_tries =
                    Regex.Match(html, @"name=""unrecognized_tries"" value=""(.*?)""").Groups[1].Value;

                var urlParams = new RequestParams();

                urlParams["pass"] = pass;
                urlParams["email"] = email;
                urlParams["jazoest"] = jazoest;
                urlParams["lsd"] = lsd;
                urlParams["m_ts"] = m_ts;
                urlParams["li"] = li;
                urlParams["try_number"] = try_number;
                urlParams["unrecognized_tries"] = unrecognized_tries;

                request.AddHeader("authority", "d.facebook.com");
                request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                request.AddHeader("accept-language", "en-US,en;q=0.9");
                request.AddHeader("sec-ch-ua-mobile", "?0");
                request.AddHeader("sec-fetch-dest", "document");
                request.AddHeader("sec-fetch-mode", "navigate");
                request.AddHeader("sec-fetch-site", "none");
                request.AddHeader("sec-fetch-user", "?1");
                request.AddHeader("upgrade-insecure-requests", "1");
                request.AddHeader("user-agent", ua);

                respone = request.Post("https://d.facebook.com" + action, urlParams);

                html = respone.ToString();

                cookie = respone.Cookies.GetCookieHeader(respone.Address);

                if (cookie.Contains("c_user"))
                {
                    return cookie;
                }

                request.AddHeader("authority", "d.facebook.com");
                request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                request.AddHeader("accept-language", "en-US,en;q=0.9");
                request.AddHeader("sec-ch-ua-mobile", "?0");
                request.AddHeader("sec-fetch-dest", "document");
                request.AddHeader("sec-fetch-mode", "navigate");
                request.AddHeader("sec-fetch-site", "none");
                request.AddHeader("sec-fetch-user", "?1");
                request.AddHeader("upgrade-insecure-requests", "1");
                request.AddHeader("user-agent", ua);

                respone = request.Get("https://d.facebook.com/login/checkpoint/");

                html = respone.ToString();

                cookie = respone.Cookies.GetCookieHeader(respone.Address);

                if (cookie.Contains("c_user"))
                {
                    return cookie;
                }

                html = Regex
                    .Match(html, @"<form method=""post"" action=""/login/checkpoint/""(.*?)</form>").Value;

                if (!string.IsNullOrEmpty(html))
                {
                    string fb_dtsg = Regex.Matches(html, @"name=""fb_dtsg"" value=""(.*?)""")[1].Groups[1].Value;

                    string approvals_code = _twoFactorAuthentication.GetCode2Fa(secretKey);

                    action = HttpUtility.HtmlDecode(Regex.Match(html, @"action=""(.*?)""").Groups[1].Value);

                    jazoest = Regex.Match(html, @"name=""jazoest"" value=""(.*?)""").Groups[1].Value;

                    string nh = Regex.Match(html, @"name=""nh"" value=""(.*?)""").Groups[1].Value;

                    string codes_submitted = Regex.Match(html, @"name=""codes_submitted"" value=""(.*?)""").Groups[1].Value;

                    urlParams = new RequestParams();
                    urlParams["fb_dtsg"] = fb_dtsg;
                    urlParams["jazoest"] = jazoest;
                    urlParams["approvals_code"] = approvals_code;
                    urlParams["codes_submitted"] = codes_submitted;
                    urlParams["submit[Submit Code]"] = "Submit Code";
                    urlParams["checkpoint_data"] = "";
                    urlParams["nh"] = nh;
                    urlParams["fb_dtsg"] = fb_dtsg;
                    urlParams["jazoest"] = jazoest;

                    request.AddHeader("authority", "d.facebook.com");
                    request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                    request.AddHeader("accept-language", "en-US,en;q=0.9");
                    request.AddHeader("sec-ch-ua-mobile", "?0");
                    request.AddHeader("sec-fetch-dest", "document");
                    request.AddHeader("sec-fetch-mode", "navigate");
                    request.AddHeader("sec-fetch-site", "none");
                    request.AddHeader("sec-fetch-user", "?1");
                    request.AddHeader("upgrade-insecure-requests", "1");
                    request.AddHeader("user-agent", ua);

                    respone = request.Post("https://d.facebook.com" + action, urlParams); // Luu thiet bi

                    html = respone.ToString();

                    cookie = respone.Cookies.GetCookieHeader(respone.Address);

                    if (cookie.Contains("c_user"))
                    {
                        return cookie;
                    }

                    match = Regex.Match(html, @"<form method=""post"" action=""/login/checkpoint/""(.*?)</form>");

                    html = match.Value;

                    if (!string.IsNullOrEmpty(html))
                    {
                        fb_dtsg = Regex.Matches(html, @"name=""fb_dtsg"" value=""(.*?)""")[1].Groups[1].Value;

                        action = HttpUtility.HtmlDecode(Regex.Match(html, @"action=""(.*?)""").Groups[1].Value);

                        jazoest = Regex.Match(html, @"name=""jazoest"" value=""(.*?)""").Groups[1].Value;

                        string name_action_selected =
                            Regex.Match(html, @"name=""name_action_selected"" value=""(.*?)"" checked=""1"" ").Groups[1].Value;

                        nh = Regex.Match(html, @"name=""nh"" value=""(.*?)""").Groups[1].Value;

                        urlParams = new RequestParams();
                        urlParams["fb_dtsg"] = fb_dtsg;
                        urlParams["jazoest"] = jazoest;
                        urlParams["name_action_selected"] = name_action_selected;
                        urlParams["submit[Continue]"] = "Continue";
                        urlParams["checkpoint_data"] = "";
                        urlParams["nh"] = nh;
                        urlParams["fb_dtsg"] = fb_dtsg;
                        urlParams["jazoest"] = jazoest;

                        request.AddHeader("authority", "d.facebook.com");
                        request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                        request.AddHeader("accept-language", "en-US,en;q=0.9");
                        request.AddHeader("sec-ch-ua-mobile", "?0");
                        request.AddHeader("sec-fetch-dest", "document");
                        request.AddHeader("sec-fetch-mode", "navigate");
                        request.AddHeader("sec-fetch-site", "none");
                        request.AddHeader("sec-fetch-user", "?1");
                        request.AddHeader("upgrade-insecure-requests", "1");
                        request.AddHeader("user-agent", ua);

                        respone = request.Post("https://d.facebook.com" + action, urlParams);

                        html = respone.ToString();

                        cookie = respone.Cookies.GetCookieHeader(respone.Address);

                        if (cookie.Contains("c_user"))
                        {
                            return cookie;
                        }

                        html = Regex.Match(html, @"<form method=""post"" action=""/login/checkpoint/""(.*?)</form>").Value;

                        if (!string.IsNullOrEmpty(html))
                        {
                            fb_dtsg = Regex.Matches(html, @"name=""fb_dtsg"" value=""(.*?)""")[1].Groups[1].Value;

                            action = HttpUtility.HtmlDecode(Regex.Match(html, @"action=""(.*?)""").Groups[1].Value);

                            jazoest = Regex.Match(html, @"name=""jazoest"" value=""(.*?)""").Groups[1].Value;

                            nh = Regex.Match(html, @"name=""nh"" value=""(.*?)""").Groups[1].Value;

                            urlParams = new RequestParams();
                            urlParams["fb_dtsg"] = fb_dtsg;
                            urlParams["jazoest"] = jazoest;
                            urlParams["submit[Continue]"] = "Continue";
                            urlParams["checkpoint_data"] = "";
                            urlParams["nh"] = nh;
                            urlParams["fb_dtsg"] = fb_dtsg;
                            urlParams["jazoest"] = jazoest;

                            request.AddHeader("authority", "d.facebook.com");
                            request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                            request.AddHeader("accept-language", "en-US,en;q=0.9");
                            request.AddHeader("sec-ch-ua-mobile", "?0");
                            request.AddHeader("sec-fetch-dest", "document");
                            request.AddHeader("sec-fetch-mode", "navigate");
                            request.AddHeader("sec-fetch-site", "none");
                            request.AddHeader("sec-fetch-user", "?1");
                            request.AddHeader("upgrade-insecure-requests", "1");
                            request.AddHeader("user-agent", ua);

                            respone = request.Post("https://d.facebook.com" + action, urlParams);

                            html = respone.ToString();

                            cookie = respone.Cookies.GetCookieHeader(respone.Address);

                            if (cookie.Contains("c_user"))
                            {
                                return cookie;
                            }


                            html = Regex.Match(html, @"<form method=""post"" action=""/login/checkpoint/""(.*?)</form>").Value;
                            if (!string.IsNullOrEmpty(html))
                            {
                                fb_dtsg = Regex.Matches(html, @"name=""fb_dtsg"" value=""(.*?)""")[1].Groups[1].Value;

                                action = HttpUtility.HtmlDecode(Regex.Match(html, @"action=""(.*?)""").Groups[1].Value);

                                jazoest = Regex.Match(html, @"name=""jazoest"" value=""(.*?)""").Groups[1].Value;

                                nh = Regex.Match(html, @"name=""nh"" value=""(.*?)""").Groups[1].Value;

                                urlParams = new RequestParams();
                                urlParams["fb_dtsg"] = fb_dtsg;
                                urlParams["jazoest"] = jazoest;
                                urlParams["submit[This was me]"] = "This was me";
                                urlParams["checkpoint_data"] = "";
                                urlParams["nh"] = nh;
                                urlParams["fb_dtsg"] = fb_dtsg;
                                urlParams["jazoest"] = jazoest;

                                request.AddHeader("authority", "d.facebook.com");
                                request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                                request.AddHeader("accept-language", "en-US,en;q=0.9");
                                request.AddHeader("sec-ch-ua-mobile", "?0");
                                request.AddHeader("sec-fetch-dest", "document");
                                request.AddHeader("sec-fetch-mode", "navigate");
                                request.AddHeader("sec-fetch-site", "none");
                                request.AddHeader("sec-fetch-user", "?1");
                                request.AddHeader("upgrade-insecure-requests", "1");
                                request.AddHeader("user-agent", ua);

                                respone = request.Post("https://d.facebook.com" + action, urlParams);

                                html = respone.ToString();

                                cookie = respone.Cookies.GetCookieHeader(respone.Address);

                                if (cookie.Contains("c_user"))
                                {
                                    return cookie;
                                }


                                html = Regex.Match(html, @"<form method=""post"" action=""/login/checkpoint/""(.*?)</form>").Value;

                                if (!string.IsNullOrEmpty(html))
                                {
                                    fb_dtsg = Regex.Matches(html, @"name=""fb_dtsg"" value=""(.*?)""")[1].Groups[1].Value;

                                    action = HttpUtility.HtmlDecode(Regex.Match(html, @"action=""(.*?)""").Groups[1].Value);

                                    jazoest = Regex.Match(html, @"name=""jazoest"" value=""(.*?)""").Groups[1].Value;

                                    name_action_selected =
                                        Regex.Match(html, @"name=""name_action_selected"" value=""(.*?)"" checked=""1"" ").Groups[1].Value;

                                    nh = Regex.Match(html, @"name=""nh"" value=""(.*?)""").Groups[1].Value;

                                    urlParams = new RequestParams();
                                    urlParams["fb_dtsg"] = fb_dtsg;
                                    urlParams["jazoest"] = jazoest;
                                    urlParams["name_action_selected"] = name_action_selected;
                                    urlParams["submit[Continue]"] = "Continue";
                                    urlParams["checkpoint_data"] = "";
                                    urlParams["nh"] = nh;
                                    urlParams["fb_dtsg"] = fb_dtsg;
                                    urlParams["jazoest"] = jazoest;

                                    request.AddHeader("authority", "d.facebook.com");
                                    request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                                    request.AddHeader("accept-language", "en-US,en;q=0.9");
                                    request.AddHeader("sec-ch-ua-mobile", "?0");
                                    request.AddHeader("sec-fetch-dest", "document");
                                    request.AddHeader("sec-fetch-mode", "navigate");
                                    request.AddHeader("sec-fetch-site", "none");
                                    request.AddHeader("sec-fetch-user", "?1");
                                    request.AddHeader("upgrade-insecure-requests", "1");
                                    request.AddHeader("user-agent", ua);

                                    respone = request.Post("https://d.facebook.com" + action, urlParams);

                                    html = respone.ToString();

                                    cookie = respone.Cookies.GetCookieHeader(respone.Address);

                                    if (cookie.Contains("c_user"))
                                    {
                                        return cookie;
                                    }

                                    request.AddHeader("authority", "d.facebook.com");
                                    request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                                    request.AddHeader("accept-language", "en-US,en;q=0.9");
                                    request.AddHeader("sec-ch-ua-mobile", "?0");
                                    request.AddHeader("sec-fetch-dest", "document");
                                    request.AddHeader("sec-fetch-mode", "navigate");
                                    request.AddHeader("sec-fetch-site", "none");
                                    request.AddHeader("sec-fetch-user", "?1");
                                    request.AddHeader("upgrade-insecure-requests", "1");
                                    request.AddHeader("user-agent", ua);

                                    respone = request.Get("https://d.facebook.com");

                                    html = respone.ToString();

                                    cookie = respone.Cookies.GetCookieHeader(respone.Address);

                                    if (cookie.Contains("c_user"))
                                    {
                                        return cookie;
                                    }
                                }

                            }

                        }

                    }

                }
            }
        }
        catch (Exception e)
        {
            Crashes.TrackError(e);
        }

        return String.Empty;
    }
}