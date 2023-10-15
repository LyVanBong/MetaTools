using Leaf.xNet;
using OtpNet;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace MetaTools.Consoles;

public class FacebookeHelper
{

    public static async Task<string> GetAccessTokenEaab(string cookie, string ua)
    {
        using (HttpRequest request = new HttpRequest())
        {
            request.AddHeader("cookie", cookie);
            request.AddHeader("User-Agent", ua);

            var response = request.Get(
                "https://www.facebook.com/dialog/oauth?scope=user_about_me,pages_read_engagement,user_actions.books,user_actions.fitness,user_actions.music,user_actions.news,user_actions.video,user_activities,user_birthday,user_education_history,user_events,user_friends,user_games_activity,user_groups,user_hometown,user_interests,user_likes,user_location,user_managed_groups,user_photos,user_posts,user_relationship_details,user_relationships,user_religion_politics,user_status,user_tagged_places,user_videos,user_website,user_work_history,email,manage_notifications,manage_pages,publish_actions,publish_pages,read_friendlists,read_insights,read_page_mailboxes,read_stream,rsvp_event,read_mailbox&response_type=token&client_id=124024574287414&redirect_uri=fb124024574287414://authorize/&sso_key=com&display=&fbclid=IwAR1KPwp2DVh2Cu7KdeANz-dRC_wYNjjHk5nR5F-BzGGj7-gTnKimAmeg08k");

            var html = response.ToString();

            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "/index4.html", html);

        }

        return string.Empty;
    }

    public static string GetTokenEAAB(string cookie, string ua)
    {
        using (HttpRequest request = new HttpRequest())
        {
            //request.UserAgent = ua;
            //var ck = SetCookies(cookie);
            //request.Cookies = new CookieStorage(container: ck);

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

                string url = Regex.Match(html, @"window.location.replace\(""(.*?)""").Groups[1].Value;

                url = url.Replace("\\", "");

                respone = request.Get(url);

                html = respone.ToString();

                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "/index3.html", html);

                if (html.Contains("EAA"))
                {
                    return Regex.Match(html, @"window.__accessToken=""(.*?)""").Groups[1].Value;
                }
            }
        }

        return string.Empty;
    }


    public static bool CheckCookie(string cookie, string ua)
    {
        using (HttpRequest request = new HttpRequest())
        {
            request.AddHeader("cookie", cookie);
            request.AddHeader("User-Agent", ua);

            var respone = request.Get("https://d.facebook.com/profile.php");

            string html = respone?.ToString();

            string cke = respone?.Cookies?.GetCookieHeader(respone.Address);

            if (!string.IsNullOrEmpty(html) && html.Contains("mbasic_logout_button"))
            {
                return true;
            }
        }

        return false;
    }

    public static string Get2Fa(string secretKey)
    {
        Totp totp = new Totp(Base32Encoding.ToBytes(secretKey.Replace(" ", "")));
        return totp.ComputeTotp(DateTime.UtcNow);
    }
    public static async Task<string> Login(string email, string pass, string code2fa, string ua)
    {
        using (HttpRequest request = new HttpRequest())
        {
            request.UserAgent = "Mozilla/5.0 (X11; Linux i686; rv:49.0) Gecko/20100101 Firefox/49.0";

            var respone = request.Get("https://d.facebook.com/login.php");

            string html = respone.ToString();

            string cookie = respone.Cookies.GetCookieHeader(respone.Address);

            if (cookie.Contains("c_user"))
            {
                return cookie;
            }

            var match = Regex.Match(html, @"<form(.*?)</form>");

            html = match.Value;

            string action = HttpUtility.HtmlDecode(Regex.Match(html, @"action=""(.*?)""").Groups[1].Value);

            string jazoest = Regex.Match(html, @"name=""jazoest"" value=""(.*?)""").Groups[1].Value;

            string lsd = Regex.Match(html, @"name=""lsd"" value=""(.*?)""").Groups[1].Value;

            string m_ts = Regex.Match(html, @"name=""m_ts"" value=""(.*?)""").Groups[1].Value;

            string li = Regex.Match(html, @"name=""li"" value=""(.*?)""").Groups[1].Value;

            string try_number = Regex.Match(html, @"name=""try_number"" value=""(.*?)""").Groups[1].Value;

            string unrecognized_tries = Regex.Match(html, @"name=""unrecognized_tries"" value=""(.*?)""").Groups[1].Value;

            var urlParams = new RequestParams();

            urlParams["pass"] = pass;
            urlParams["email"] = email;
            urlParams["jazoest"] = jazoest;
            urlParams["lsd"] = lsd;
            urlParams["m_ts"] = m_ts;
            urlParams["li"] = li;
            urlParams["try_number"] = try_number;
            urlParams["unrecognized_tries"] = unrecognized_tries;

            respone = request.Post("https://d.facebook.com" + action, urlParams);

            html = respone.ToString();

            cookie = respone.Cookies.GetCookieHeader(respone.Address);

            if (cookie.Contains("c_user"))
            {
                return cookie;
            }

            respone = request.Get("https://d.facebook.com/login/checkpoint/");

            html = respone.ToString();

            cookie = respone.Cookies.GetCookieHeader(respone.Address);

            if (cookie.Contains("c_user"))
            {
                return cookie;
            }

            html = Regex
                .Match(html, @"<form method=""post"" action=""/login/checkpoint/""(.*?)</form>").Value;

            string fb_dtsg = Regex.Matches(html, @"name=""fb_dtsg"" value=""(.*?)""")[1].Groups[1].Value;

            string approvals_code = Get2Fa(code2fa);

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

            respone = request.Post("https://d.facebook.com" + action, urlParams);

            html = respone.ToString();

            cookie = respone.Cookies.GetCookieHeader(respone.Address);

            if (cookie.Contains("c_user"))
            {
                return cookie;
            }

            html = Regex.Match(html, @"<form method=""post"" action=""/login/checkpoint/""(.*?)</form>").Value;

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

            respone = request.Post("https://d.facebook.com" + action, urlParams);

            html = respone.ToString();

            cookie = respone.Cookies.GetCookieHeader(respone.Address);

            if (cookie.Contains("c_user"))
            {
                return cookie;
            }

            html = Regex.Match(html, @"<form method=""post"" action=""/login/checkpoint/""(.*?)</form>").Value;

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

            respone = request.Post("https://d.facebook.com" + action, urlParams);

            html = respone.ToString();

            cookie = respone.Cookies.GetCookieHeader(respone.Address);

            if (cookie.Contains("c_user"))
            {
                return cookie;
            }


            html = Regex.Match(html, @"<form method=""post"" action=""/login/checkpoint/""(.*?)</form>").Value;

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

            respone = request.Post("https://d.facebook.com" + action, urlParams);

            html = respone.ToString();

            cookie = respone.Cookies.GetCookieHeader(respone.Address);

            if (cookie.Contains("c_user"))
            {
                return cookie;
            }


            html = Regex.Match(html, @"<form method=""post"" action=""/login/checkpoint/""(.*?)</form>").Value;

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

            respone = request.Post("https://d.facebook.com" + action, urlParams);

            html = respone.ToString();

            cookie = respone.Cookies.GetCookieHeader(respone.Address);

            if (cookie.Contains("c_user"))
            {
                return cookie;
            }

            respone = request.Get("https://d.facebook.com");

            html = respone.ToString();

            cookie = respone.Cookies.GetCookieHeader(respone.Address);

            if (cookie.Contains("c_user"))
            {
                return cookie;
            }
        }

        return null;
    }
}