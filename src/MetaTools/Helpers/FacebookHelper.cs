using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace MetaTools.Helpers
{
    public class FacebookHelper
    {
        public static async Task<string> GetLinkLikeComment(string cookie, string ua, string idComment)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://d.facebook.com/" + idComment);
            request.Headers.Add("authority", "d.facebook.com");
            request.Headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
            request.Headers.Add("cookie", cookie);
            request.Headers.Add("dpr", "1");
            request.Headers.Add("sec-fetch-dest", "document");
            request.Headers.Add("sec-fetch-mode", "navigate");
            request.Headers.Add("sec-fetch-site", "none");
            request.Headers.Add("sec-fetch-user", "?1");
            request.Headers.Add("upgrade-insecure-requests", "1");
            request.Headers.Add("user-agent", ua);
            request.Headers.Add("viewport-width", Random.Shared.Next(500, 1200) + "");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var html = await response.Content.ReadAsStringAsync();

            var match = Regex.Match(html, @"\/a\/comment\.php\?like_comment_id=" + idComment + "(.*?)\"");
            if (match.Success)
            {
                string url = "/a/comment.php?like_comment_id=" + idComment +
                             match.Groups[1].Value;
                url = HttpUtility.HtmlDecode(url);
                return url;
            }
            return null;
        }

        public static async Task BuffLikeComment(string cookie, string url, string ua)
        {
            var client = new HttpClient();
            url = "https://d.facebook.com" + url;
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("authority", "d.facebook.com");
            request.Headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
            request.Headers.Add("cookie", cookie);
            request.Headers.Add("dpr", "1");
            request.Headers.Add("sec-fetch-dest", "document");
            request.Headers.Add("sec-fetch-mode", "navigate");
            request.Headers.Add("sec-fetch-site", "none");
            request.Headers.Add("sec-fetch-user", "?1");
            request.Headers.Add("upgrade-insecure-requests", "1");
            request.Headers.Add("user-agent", ua);
            request.Headers.Add("viewport-width", Random.Shared.Next(500, 1200) + "");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var html = await response.Content.ReadAsStringAsync();
        }
    }
}