using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MetaTools.Modules.Comments.Helpers
{
    public class FacebookHelper
    {
        public static async Task<string> LikeComment2(string cookie, string fb_dtsg, string jazoest, string linkPosts, string uid, string ua)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://www.facebook.com/api/graphql/");
            request.Headers.Add("cookie", cookie);
            request.Headers.Add("origin", "https://www.facebook.com");
            request.Headers.Add("referer", linkPosts);
            request.Headers.Add("sec-fetch-mode", "cors");
            request.Headers.Add("sec-fetch-site", "same-origin");
            request.Headers.Add("user-agent", ua);
            request.Headers.Add("viewport-width", Random.Shared.Next(500, 1000) + "");
            request.Headers.Add("x-asbd-id", "129477");
            request.Headers.Add("x-fb-friendly-name", "CometUFIFeedbackReactMutation");
            request.Headers.Add("x-fb-lsd", "xpUiwGHkQzYXmvTtX8wWRD");
            var collection = new List<KeyValuePair<string, string>>();
            collection.Add(new("av", uid));
            collection.Add(new("__user", uid));
            collection.Add(new("__a", "1"));
            collection.Add(new("__req", "1t"));
            collection.Add(new("__hs", "19634.HYP:comet_pkg.2.1..2.1"));
            collection.Add(new("dpr", "2"));
            collection.Add(new("__ccg", "EXCELLENT"));
            collection.Add(new("__rev", "1009033342"));
            collection.Add(new("__s", "dfe361:3176du:v3qobm"));
            collection.Add(new("__hsi", "7286137514059345976"));
            collection.Add(new("__dyn", "7AzHK4HzE4e5Q1ryaxG4VuC2-m1xDwAxu13wFwhUKbgS3q5UObwNwnof8boG0x8bo6u3y4o2vyE5O0BU2_CxS320om78bbwto88422y11xmfz83WwgEcEhwGxu782lwv89kbxS2218wc61awkovwRwlE-U2exi4UaEW2au1jxS6FobrwKxm5oe8cEW4-5pUfEe88o4Wm7-8wywdG7FoarCwLyESE6C14wwwOg2cwMwhEkxe3u364UrwFg662S269wkopg6C13whEeE"));
            collection.Add(new("__csr", "hsA3z2caNYRPjNAhRWsyN5lYIyFp7OqmPsDNLSXcy4mSIHKBbjayql5F27KR_FCQhu_Fpk8HvjiJ9aUC8QahptaDm8Wz9FkXLGHG9iWumfACAG-eyF448-58izK6WAhAuFoKmu7UDwQxPByVmte6FUO2-2Su78BkiKeh8Twwy8owEyo8UdovwABxm8wjoWqUkG48O2aq7Eyh29o8FVKim1qzbxyexu323ajxS3J0aa1nwxxu3m7E7amU-4U8UcoO3S8z8sx60B86a19wmUswrUjxm08IwSwdq0AWhA0xpJaF9o5611w8i0E81hE5l00Ybw3B80Hq029x00a6q1Tw66w4ig0iHwbu0h21Aggg4C0lu0li0dZwCwa202QS07ZGzo0nEyy06Zw3FU08VE0Ze0bPwrV7c9y468cE1Po2vgeoCu0hu0GE4Cqq4U32w"));
            collection.Add(new("__comet_req", "15"));
            collection.Add(new("fb_dtsg", fb_dtsg));
            collection.Add(new("jazoest", jazoest));
            collection.Add(new("lsd", "xpUiwGHkQzYXmvTtX8wWRD"));
            collection.Add(new("__aaid", "0"));
            collection.Add(new("__spin_r", "1009033342"));
            collection.Add(new("__spin_b", "trunk"));
            collection.Add(new("__spin_t", "1696436087"));
            collection.Add(new("qpl_active_flow_ids", "431626709"));
            collection.Add(new("fb_api_caller_class", "RelayModern"));
            collection.Add(new("fb_api_req_friendly_name", "CometUFIFeedbackReactMutation"));
            collection.Add(new("variables", "{\"input\":{\"attribution_id_v2\":\"CometGroupPermalinkRoot.react,comet.group.permalink,unexpected,1696436097956,514642,2361831622,;CometActivityLogMainContentRoot.react,comet.ActivityLog.CometActivityLogMainContentRoute,via_cold_start,1696436090099,320649,,\",\"feedback_id\":\"ZmVlZGJhY2s6NjU5ODgzNjQ2MDUxOTk5XzY4MDU2MzYxMzk4NDAwMg==\",\"feedback_reaction_id\":\"1635855486666999\",\"feedback_source\":\"OBJECT\",\"feedback_referrer\":\"/checkpoint/\",\"is_tracking_encrypted\":true,\"tracking\":[\"AZVtkq-llgQPx-f18DAlCJ4phwAqSPLawF6Cfm5GekkoZbPvep01ZCebHWOKzA9ZppMiFHbcbdKE3OyJUeOjo13HXmFQ0RNhqlNI8l4JtM84m_cbcj8pzhrrL9AKDkLZXYchc20Q7vbV6r_Tkg5GwChnZ_mlx9_2XQG_wCN4mHB3WbDIp-15soPiTBCBFPhVdt5DxtQEbkIzFkC5TY4fw3ILtVODzKiBF0zJkGy1jtQFMYLl0pp2HUVCAWwbpUkyB7qGXWJhxCEzpZjYIOYKHIO4CcooCGw5zfKcUwvXvnW5053i4WZ69QUlDpAYeQ2ZcOMiqFo5fYLlc_WmKmLs69T5LOmtUT4jloY387q17QT1KUV1RFBJDlwa1rYfeVF-augd6d_VUxnVNJL2HrR7TPVHznhkIAy8vHqQjRB_LplgHz3EHX_b05VsRGrs_kR54MadBxSx6Ai31fLDFbw83XOdjck29Cxno1e-fEAKnzo78W6yolxT_PeqeOJUNnphlg0gapKqARR2fEzsuhn9-2rF4wYhc7irtX5MHDn0tq7H7LgzAKMcZbntX8O2ukUS3Nqtox88LdJEr_DeNf5YoRAyFomVIERZX6P6jddbmoZyzuvziJ4iztXGy3p249FOZ_h1rULIrqA3TW4xFmuUYWxGXDcnHjjyAFPgS3BMgQ9gyDiLtlmlGB-bVMLnsV0ZJ_tFK-Yer3zZKQUeGIw3savgQHAe96pWSvG8PD2DBApsX-cDHrHapSpbt0vTTn0MGXpsxddDTZ1DmRR7Nt6wfOxG4KnVulznRe35PPg6ZUwsbFLBCs93l5b6NkLteWpyNbYcXhMQbPalvoBfAknLwY4QRB2s-O5uUxJw6eOT-LCUGBupT1ZM-IQ4N9pwC7H3_Wg7HjPJ8kHLO5ift7POavFW4RmuCTnuQG0-eKYblXU0RSjrXeUUy5SrkpXtVox1Qc0\"],\"session_id\":\"5ec84d77-2f5f-4878-8ca0-416cf53995b9\",\"actor_id\":\"" + uid + "\",\"client_mutation_id\":\"3\"},\"useDefaultActor\":false,\"scale\":2}"));
            collection.Add(new("server_timestamps", "true"));
            collection.Add(new("doc_id", "6324189467690403"));
            collection.Add(new("fb_api_analytics_tags", "[\"qpl_active_flow_ids=431626709\"]"));
            var content = new FormUrlEncodedContent(collection);
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        public static void LikeComment(string cookie, string fb_dtsg, string jazoest, string linkPosts, string uid)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://www.facebook.com/api/graphql/");
            request.Headers.Add("authority", "www.facebook.com");
            request.Headers.Add("accept", "*/*");
            request.Headers.Add("accept-language", "en-US,en;q=0.9");
            //request.Headers.Add("content-type", "application/x-www-form-urlencoded");
            request.Headers.Add("cookie", cookie);
            request.Headers.Add("dpr", "2");
            request.Headers.Add("origin", "https://www.facebook.com");
            request.Headers.Add("referer", linkPosts);
            request.Headers.Add("sec-ch-prefers-color-scheme", "light");
            request.Headers.Add("sec-ch-ua", "\"Chromium\";v=\"116\", \"Not)A;Brand\";v=\"24\", \"Google Chrome\";v=\"116\"");
            request.Headers.Add("sec-ch-ua-full-version-list", "\"Chromium\";v=\"116.0.5845.188\", \"Not)A;Brand\";v=\"24.0.0.0\", \"Google Chrome\";v=\"116.0.5845.188\"");
            request.Headers.Add("sec-ch-ua-mobile", "?0");
            request.Headers.Add("sec-ch-ua-model", "\"\"");
            request.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
            request.Headers.Add("sec-ch-ua-platform-version", "\"15.0.0\"");
            request.Headers.Add("sec-fetch-dest", "empty");
            request.Headers.Add("sec-fetch-mode", "cors");
            request.Headers.Add("sec-fetch-site", "same-origin");
            request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/116.0.0.0 Safari/537.36");
            request.Headers.Add("viewport-width", "885");
            request.Headers.Add("x-asbd-id", "129477");
            request.Headers.Add("x-fb-friendly-name", "CometUFIFeedbackReactMutation");
            request.Headers.Add("x-fb-lsd", "GsrfQHeGANpmiVG8I8A8gc");
            var collection = new List<KeyValuePair<string, string>>();
            collection.Add(new("av", uid));
            collection.Add(new("__user", uid));
            collection.Add(new("__a", "1"));
            collection.Add(new("__req", "64"));
            collection.Add(new("__hs", "19617.HYP:comet_pkg.2.1..2.1"));
            collection.Add(new("dpr", "2"));
            collection.Add(new("__ccg", "EXCELLENT"));
            collection.Add(new("__rev", "1008685377"));
            collection.Add(new("__s", "41blti:87uvkm:4c9pql"));
            collection.Add(new("__hsi", "7279655111043959189"));
            collection.Add(new("__dyn", "7AzHK4HzEmgDx-5Q1ryaxG4QjFwLBwopU98nwgUao4ubyQdwSAxacyU8EW1twYwJyEiwsobo5a58e8hw8u5UB0n82nwb-q7oc81xoszUsK1Rwwwg8a8465o-cw8a1TwgEcEhwGwQw9m1cwLwBgK7o8417wIwtouwiE567Udo5qfK0zEkxe2GexeeDwkUtxGm2SUbElxm3y3aexfxmu3W3jU8o4qum7-8wywdG7FoarCwLyESE6C14wwwOg2cwMwhEkxe3u364UrwFgbUcobo8oC1hxB0qo4e16wWw"));
            collection.Add(new("__csr", "gywdkesIegml1j94jSxiMz5hqT6l4VkGimOmPiaysyTFeTFaAvtdroxdFtkmh4jcCBgLhukX-kxaClBGAyd6CiQSAjFUx6SQG-XLDUKJ-trKhpa_GGAGch9da9hVm9hK9DWmAbJd4gOnSJ4B4lzqBDGVUyhxiEbEXy-mdzGpke-ihLFpWzumayFGGqVppQ8yU-549UuAiCxiAiazUG4FV8C9UFxi448CzUzxeUS4eEZzA9K_ByUK8zqAgnyEG9wkEK9gkDK6UR5UScBK9xGm5orK_yogx26bx-cDVrWKdx2364448y4Q54m2OaxuGAx226i9xi26mahopAG27yo8-6EGiErzbK6HgmyofoixmUnzUpKA3yezkm17xS3d0au0hm0AE14k0fvw2Do7W09cw2XE0x6054E3soW2Cca0nPw5iK04JU1R8oTg06N60lV4w0yK4DgGl03ME1Do3hx-08qw2bY-hx6Cis9JkLaxcgUW4Q2swcGzk4XgJ09u30M2iw2MpBwUiwc29BAg421Tx20ny1Kw-io1xrwywbO6U8U2QCw34pp81lE3kw3hQ0lKWQ0hS4o2tyk7o0Hm1awiUkw5EzlzEjwgWw35GK4o3BhpQ9P0ca0VUco3gxFDAhQ84w2o8W05SFQ4opw2V81T3024U2kwcJyE2Sw2T9U6e2C0ME7N0lOwait0BwaW08Sw38E"));
            collection.Add(new("__comet_req", "15"));
            collection.Add(new("fb_dtsg", fb_dtsg));
            collection.Add(new("jazoest", jazoest));
            collection.Add(new("__spin_r", "1008685377"));
            collection.Add(new("__spin_b", "trunk"));
            collection.Add(new("__spin_t", "1694926785"));
            collection.Add(new("fb_api_caller_class", "RelayModern"));
            collection.Add(new("fb_api_req_friendly_name", "CometUFIFeedbackReactMutation"));
            collection.Add(new("variables", "{\"input\":{\"attribution_id_v2\":\"CometSinglePostRoot.react,comet.post.single,unexpected,1694928183980,828085,,;CometActivityLogMainContentRoot.react,comet.ActivityLog.CometActivityLogMainContentRoute,unexpected,1694928180785,290353,,;CometActivityLogMainContentRoot.react,comet.ActivityLog.CometActivityLogMainContentRoute,unexpected,1694928177297,761967,,;ProfileCometTimelineListViewRoot.react,comet.profile.timeline.list,tap_bookmark,1694928174267,942401,100090326434793,\",\"feedback_id\":\"ZmVlZGJhY2s6NjUzNTgzMzEzNTAzODM2XzMxODc3MDQ3NzM3NTQzOA==\",\"feedback_reaction_id\":\"1635855486666999\",\"feedback_source\":\"OBJECT\",\"is_tracking_encrypted\":true,\"tracking\":[\"AZWPWejC0opvofimnAIvU553MJhJ8BthQrSIGs13vtUq_TiTAM4YpWymLnd0qyhMqyozTMHpu2mjD_L5Katn5lza4nQLbZoa3A_IMG7m9Wk8ZZ7bYVUJwKv7Sv_6hMjLMpV7X5-QnVM16Q4cFXC5ew-82HU-Cy2tacz4LKoNyqei7MyAyoVIwXsKK7vUfJuAA0tBeAAdV8AEBC4JLq2x-TNtm1IYTJkHt_5k7XOr1PLfzEEu-vDe94tbHKMEDjT9Y6Y5uHF3iy8QvaPwdoQPcrRqZf2OPyBMh6ADR-Uz2ez9ipa8AT7Hb-vCu0dvywTMNnbkNzKwbyE8Ekr_OGA06wY_SqmoQN2s68g0VGdh3lUeoyRnerdraEPwLagycJR7yRH6vc68MqHpdbTS9R2NXB57gzLKOyQyrDPl6kSW2h5SPrEhnXSo9x6QGzDVZowOfX6b_FhqIQmCfJutnkSrNokWZH5WyHMtgwxjIUyN3bRe6UCdjnUjxEOmYuCS2vWna9PTDjSf0r3r6skRQPFKxtwLlU9CpOoXm2ByQtydwMC8i71G8K4KYr7wnLwEkSsXhbVktGMiwS2e6u0A2ci-rrITwyevbotxU-vyaI0KHsufjvg8mWsIMMqu3OWYb6eYbrHYvDZx8fv9Osj9W_jwX8DuMSOFE6EUw-4hfdRuP85qtTQqLQSTfRIKv5BvV_EthEAPEyhnMPow3vQuopzHBPMXJKM-3mNedwE_eaBAedRfZ3HwAa2IddgcKoK7WcG_pn6yW51L8WuCBCxWfyiLJEWPQkj4QFbyIC39g2wmDtPcGF-8lIeIm0soZ7NxEkF4vRs-l6b6Ckyuoze8soVE5Lc3QtmEeJ6zNZL8X_jYUc_uxbZU75qOdOZg0Atz4f_3-jtrFikwCWnwVkBpOi49lD5wg98acWXTux_q6VBR24o6dkGfjxg5GaF4zFulZhEkHunwa28tCVQyqxl3lXqpDB2gu7AVuMrRPJhKzvk7Huy5bb0zB5uKKzQySFRaa26mq7WDlP71wCxw9XFOpyxlfXI6Drjvp8AwjlxKrfwz6BPF7IR3bx2Cew_oQoh23eIzs1CeNub9l_7jReK0g2_MGxeR1HeYHFEiVjzhboO_yMJv8e2RrmplYAs7Q-dQuWXby1G5qVjebmNO4C1cjTKFjVE8HFCMX8zB8FEsfacF1Ik-l3539PUlhDfkx13oq6c8F8UBMmDFWMESyRIBvtbb0ocZ\"],\"session_id\":\"78374c09-1562-476e-9163-246464229bf4\",\"actor_id\":" + uid + ",\"client_mutation_id\":\"11\"},\"useDefaultActor\":false,\"scale\":2}"));
            collection.Add(new("server_timestamps", "true"));
            collection.Add(new("doc_id", "6324189467690403"));
            var content = new FormUrlEncodedContent(collection);
            request.Content = content;
            var response = client.Send(request);
            response.EnsureSuccessStatusCode();
            var html = response.Content.ReadAsStringAsync().Result;

        }
        public static (string fb_dtsg, string jazoest) GetParam(string cookie)
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://d.facebook.com/");
            request.Headers.Add("authority", "d.facebook.com");
            request.Headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
            request.Headers.Add("accept-language", "en-US,en;q=0.9");
            request.Headers.Add("cookie", cookie);
            request.Headers.Add("dpr", "2");
            request.Headers.Add("sec-ch-prefers-color-scheme", "light");
            request.Headers.Add("sec-ch-ua", "\"Chromium\";v=\"116\", \"Not)A;Brand\";v=\"24\", \"Google Chrome\";v=\"116\"");
            request.Headers.Add("sec-ch-ua-full-version-list", "\"Chromium\";v=\"116.0.5845.188\", \"Not)A;Brand\";v=\"24.0.0.0\", \"Google Chrome\";v=\"116.0.5845.188\"");
            request.Headers.Add("sec-ch-ua-mobile", "?0");
            request.Headers.Add("sec-ch-ua-model", "\"\"");
            request.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
            request.Headers.Add("sec-ch-ua-platform-version", "\"15.0.0\"");
            request.Headers.Add("sec-fetch-dest", "document");
            request.Headers.Add("sec-fetch-mode", "navigate");
            request.Headers.Add("sec-fetch-site", "none");
            request.Headers.Add("sec-fetch-user", "?1");
            request.Headers.Add("upgrade-insecure-requests", "1");
            request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/116.0.0.0 Safari/537.36");
            request.Headers.Add("viewport-width", "885");
            var response = client.Send(request);
            response.EnsureSuccessStatusCode();
            var html = response.Content.ReadAsStringAsync().Result;

            var fbDtsg = Regex.Matches(html, @"<input type=""hidden"" name=""fb_dtsg"" value=""(.*?)""")[0].Groups[1].Value;
            var jazoest = Regex.Matches(html, @"<input type=""hidden"" name=""jazoest"" value=""(.*?)""")[0].Groups[1].Value;

            return (fbDtsg, jazoest);
        }
    }
}
