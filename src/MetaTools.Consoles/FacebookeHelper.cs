using System.Text;
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
    public static async Task<string> Login()
    {
        using (HttpRequest request = new HttpRequest())
        {
            string email = "100053489240893";
            string pass = "thieu123aA@";
            string code2fa = "HCIX RBJD T7CZ TB2T ITRT 66QX ZRT3 F5VN";
            string ua = "Mozilla/5.0 (X11; Linux i686; rv:49.0) Gecko/20100101 Firefox/49.0";

            request.UserAgent = "Mozilla/5.0 (X11; Linux i686; rv:49.0) Gecko/20100101 Firefox/49.0";

            var respone = request.Get("https://d.facebook.com/login.php");

            string html = respone.ToString();

            string cookie = respone.Cookies.GetCookieHeader(respone.Address);

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

            respone = request.Get("https://d.facebook.com/login/checkpoint/");

            html = respone.ToString();

            cookie = respone.Cookies.GetCookieHeader(respone.Address);

            html = Regex
                .Match(html, @"<form method=""post"" action=""/login/checkpoint/""(.*?)</form>").Value;

            string fb_dtsg = Regex.Matches(html, @"name=""fb_dtsg"" value=""(.*?)""")[1].Groups[1].Value;

            string approvals_code = Get2Fa(code2fa);

            action = HttpUtility.HtmlDecode(Regex.Match(html, @"action=""(.*?)""").Groups[1].Value);

            jazoest = Regex.Match(html, @"name=""jazoest"" value=""(.*?)""").Groups[1].Value;
            
            urlParams = new RequestParams();
            urlParams["fb_dtsg"] = fb_dtsg;
            urlParams["jazoest"] = jazoest;
            urlParams["approvals_code"] = approvals_code;
            urlParams["codes_submitted"] = "0";
            urlParams["submit[Submit Code]"] = "Submit Code";
            urlParams["nh"] = Guid.NewGuid().ToString().Replace("-", "");
            urlParams["fb_dtsg"] = fb_dtsg;
            urlParams["jazoest"] = jazoest;

            request.AddHeader("authority", "d.facebook.com");
            request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
            request.AddHeader("accept-language", "en-US,en;q=0.9");
            request.AddHeader("cache-control", "max-age=0");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("dpr", "1");
            request.AddHeader("origin", "https://d.facebook.com");
            request.AddHeader("referer", "https://d.facebook.com/login/checkpoint/?refsrc=deprecated&_rdr");
            request.AddHeader("sec-ch-prefers-color-scheme", "dark");
            request.AddHeader("sec-ch-ua-mobile", "?0");
            request.AddHeader("sec-ch-ua-model", "\"\"");
            request.AddHeader("sec-fetch-dest", "document");
            request.AddHeader("sec-fetch-mode", "navigate");
            request.AddHeader("sec-fetch-site", "same-origin");
            request.AddHeader("sec-fetch-user", "?1");
            request.AddHeader("upgrade-insecure-requests", "1");
            request.AddHeader("viewport-width", Random.Shared.Next(500, 1800) + "");

            respone = request.Post("https://d.facebook.com/login/checkpoint/", urlParams);

            html = respone.ToString();

            cookie = respone.Cookies.GetCookieHeader(respone.Address);


            respone = request.Get("https://d.facebook.com/login/checkpoint/");

            html = respone.ToString();

            cookie = respone.Cookies.GetCookieHeader(respone.Address);
        }

        return null;
    }
}