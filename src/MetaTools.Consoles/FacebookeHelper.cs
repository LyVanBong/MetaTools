using Leaf.xNet;
using OtpNet;
using System.Text.RegularExpressions;
using System.Web;

namespace MetaTools.Consoles;

public class FacebookeHelper
{
    public static string Get2Fa(string secretKey)
    {
        Totp totp = new Totp(Base32Encoding.ToBytes(secretKey.Replace(" ", "")));
        return totp.ComputeTotp(DateTime.UtcNow);
    }
    public static async Task<string> Login(string email, string pass, string code2fa,string ua)
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