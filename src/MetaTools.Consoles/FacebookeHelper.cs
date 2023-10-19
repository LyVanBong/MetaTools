namespace MetaTools.Consoles;

public class FacebookeHelper
{
    public static async Task<string> GetConversationsAsync(string token, string id, string url = null)
    {
        if (string.IsNullOrEmpty(url))
        {
            url = "https://graph.facebook.com/v18.0/" + id + "/conversations?limit=100&access_token=" + token;
        }

        var client = new HttpClient();
        var request = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, url);
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();

        return json;
    }

    public static async Task<string> GetPageInfoAsync(string token)
    {
        var client = new HttpClient();

        var request = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, "https://graph.facebook.com/v18.0/me?fields=accounts&access_token=" + token);

        var response = await client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        return json;
    }
    public static async Task<string[]> GetTokenPage(string token, string ua, string cookie)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, "https://graph.facebook.com/v18.0/me/accounts?access_token=" + token);
        request.Headers.Add("authority", "graph.facebook.com");
        request.Headers.Add("accept", "*/*");
        request.Headers.Add("accept-language", "en-US,en;q=0.9,vi;q=0.8");
        request.Headers.Add("origin", "https://developers.facebook.com");
        request.Headers.Add("referer", "https://developers.facebook.com/");
        request.Headers.Add("sec-ch-ua-mobile", "?0");
        request.Headers.Add("sec-fetch-dest", "empty");
        request.Headers.Add("sec-fetch-mode", "cors");
        request.Headers.Add("sec-fetch-site", "same-site");
        request.Headers.Add("user-agent", ua);
        request.Headers.Add("Cookie", cookie);
        var response = await client.SendAsync(request);
        if (response != null)
        {
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return Regex.Matches(json, @"""access_token"": ""(.*?)""").Select(x => x?.Groups[1]?.Value)?.ToArray();
        }

        return null;
    }
    public static async Task<string> CheckPoint_828281030927956(string cookie, string ua, string email, string passEmail)
    {
        using (HttpRequest request = new HttpRequest())
        {
            request.AddHeader("authority", "d.facebook.com");
            request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
            request.AddHeader("accept-language", "en-US,en;q=0.9");
            request.AddHeader("cookie", cookie);
            request.AddHeader("dpr", "1");
            request.AddHeader("sec-ch-prefers-color-scheme", "dark");
            request.AddHeader("sec-ch-ua-mobile", "?0");
            request.AddHeader("sec-ch-ua-model", "\"\"");
            request.AddHeader("sec-fetch-dest", "document");
            request.AddHeader("sec-fetch-mode", "navigate");
            request.AddHeader("sec-fetch-site", "none");
            request.AddHeader("sec-fetch-user", "?1");
            request.AddHeader("upgrade-insecure-requests", "1");
            request.AddHeader("user-agent", ua);
            request.AddHeader("viewport-width", Random.Shared.Next(500, 2000) + "");

            var response = request.Get("https://d.facebook.com/");

            var html = response?.ToString();

            if (html != null)
            {
                string url = Regex.Match(html, @"\/x\/checkpoint\/828281030927956\/stepper\/\?token=(.*?)""").Value;
                if (url != null)
                {
                    url = "https://d.facebook.com" + url;
                    url = HttpUtility.HtmlDecode(url);
                    request.AddHeader("authority", "d.facebook.com");
                    request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                    request.AddHeader("accept-language", "en-US,en;q=0.9");
                    request.AddHeader("cookie", cookie);
                    request.AddHeader("dpr", "1");
                    request.AddHeader("sec-ch-prefers-color-scheme", "dark");
                    request.AddHeader("sec-ch-ua-mobile", "?0");
                    request.AddHeader("sec-ch-ua-model", "\"\"");
                    request.AddHeader("sec-fetch-dest", "document");
                    request.AddHeader("sec-fetch-mode", "navigate");
                    request.AddHeader("sec-fetch-site", "none");
                    request.AddHeader("sec-fetch-user", "?1");
                    request.AddHeader("upgrade-insecure-requests", "1");
                    request.AddHeader("user-agent", ua);
                    request.AddHeader("viewport-width", Random.Shared.Next(500, 2000) + "");
                    response = request.Get(url);
                    html = response?.ToString();
                    if (html != null)
                    {
                        url = Regex.Match(html, @"\/x\/checkpoint\/828281030927956\/anti_scripting\/\?token=(.*)""").Value;
                        if (url != null)
                        {
                            url = "https://d.facebook.com" + url;
                            url = HttpUtility.HtmlDecode(url);
                            request.AddHeader("authority", "d.facebook.com");
                            request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                            request.AddHeader("accept-language", "en-US,en;q=0.9");
                            request.AddHeader("cookie", cookie);
                            request.AddHeader("dpr", "1");
                            request.AddHeader("sec-ch-prefers-color-scheme", "dark");
                            request.AddHeader("sec-ch-ua-mobile", "?0");
                            request.AddHeader("sec-ch-ua-model", "\"\"");
                            request.AddHeader("sec-fetch-dest", "document");
                            request.AddHeader("sec-fetch-mode", "navigate");
                            request.AddHeader("sec-fetch-site", "none");
                            request.AddHeader("sec-fetch-user", "?1");
                            request.AddHeader("upgrade-insecure-requests", "1");
                            request.AddHeader("user-agent", ua);
                            request.AddHeader("viewport-width", Random.Shared.Next(500, 2000) + "");
                            response = request.Get(url);
                            html = response?.ToString();
                            if (html != null)
                            {
                                url = Regex.Match(html, @"\/epsilon\/select_challenge\/async\/\?token=(.*?)""").Value;
                                if (url != null)
                                {
                                    url = "https://d.facebook.com" + url;
                                    url = HttpUtility.HtmlDecode(url);
                                    request.AddHeader("authority", "d.facebook.com");
                                    request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                                    request.AddHeader("accept-language", "en-US,en;q=0.9");
                                    request.AddHeader("cookie", cookie);
                                    request.AddHeader("dpr", "1");
                                    request.AddHeader("sec-ch-prefers-color-scheme", "dark");
                                    request.AddHeader("sec-ch-ua-mobile", "?0");
                                    request.AddHeader("sec-ch-ua-model", "\"\"");
                                    request.AddHeader("sec-fetch-dest", "document");
                                    request.AddHeader("sec-fetch-mode", "navigate");
                                    request.AddHeader("sec-fetch-site", "none");
                                    request.AddHeader("sec-fetch-user", "?1");
                                    request.AddHeader("upgrade-insecure-requests", "1");
                                    request.AddHeader("user-agent", ua);
                                    request.AddHeader("viewport-width", Random.Shared.Next(500, 2000) + "");

                                    string fb_dtsg = Regex.Match(html, @"name=""fb_dtsg"" value=""(.*?)""").Groups[1]
                                        .Value;
                                    string jazoest = Regex.Match(html, @"name=""jazoest"" value=""(.*?)""").Groups[1]
                                        .Value;

                                    var requestParams = new RequestParams();
                                    requestParams["fb_dtsg"] = fb_dtsg;
                                    requestParams["jazoest"] = jazoest;
                                    requestParams["challenge"] = "email_captcha";

                                    response = request.Post(url, requestParams);

                                    html = response?.ToString();
                                    if (html != null)
                                    {
                                        url = Regex.Match(html, @"/epsilon/sc/async/select/\?token=(.*?)""").Value;
                                        if (url != null)
                                        {
                                            url = "https://d.facebook.com" + url;
                                            url = HttpUtility.HtmlDecode(url);
                                            request.AddHeader("authority", "d.facebook.com");
                                            request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                                            request.AddHeader("accept-language", "en-US,en;q=0.9");
                                            request.AddHeader("cookie", cookie);
                                            request.AddHeader("dpr", "1");
                                            request.AddHeader("sec-ch-prefers-color-scheme", "dark");
                                            request.AddHeader("sec-ch-ua-mobile", "?0");
                                            request.AddHeader("sec-ch-ua-model", "\"\"");
                                            request.AddHeader("sec-fetch-dest", "document");
                                            request.AddHeader("sec-fetch-mode", "navigate");
                                            request.AddHeader("sec-fetch-site", "none");
                                            request.AddHeader("sec-fetch-user", "?1");
                                            request.AddHeader("upgrade-insecure-requests", "1");
                                            request.AddHeader("user-agent", ua);
                                            request.AddHeader("viewport-width", Random.Shared.Next(500, 2000) + "");

                                            fb_dtsg = Regex.Match(html, @"name=""fb_dtsg"" value=""(.*?)""").Groups[1]
                                               .Value;
                                            jazoest = Regex.Match(html, @"name=""jazoest"" value=""(.*?)""").Groups[1]
                                               .Value;

                                            requestParams = new RequestParams();
                                            requestParams["fb_dtsg"] = fb_dtsg;
                                            requestParams["jazoest"] = jazoest;
                                            requestParams["index"] = "ec:0";

                                            response = request.Post(url, requestParams);

                                            html = response?.ToString();
                                            if (html != null)
                                            {
                                                url = Regex.Match(html, @"/epsilon/sc/async/verify/\?token=(.*?)""")
                                                    .Value;
                                                if (url != null)
                                                {
                                                    url = "https://d.facebook.com" + url;
                                                    url = HttpUtility.HtmlDecode(url);
                                                    request.AddHeader("authority", "d.facebook.com");
                                                    request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                                                    request.AddHeader("accept-language", "en-US,en;q=0.9");
                                                    request.AddHeader("cookie", cookie);
                                                    request.AddHeader("dpr", "1");
                                                    request.AddHeader("sec-ch-prefers-color-scheme", "dark");
                                                    request.AddHeader("sec-ch-ua-mobile", "?0");
                                                    request.AddHeader("sec-ch-ua-model", "\"\"");
                                                    request.AddHeader("sec-fetch-dest", "document");
                                                    request.AddHeader("sec-fetch-mode", "navigate");
                                                    request.AddHeader("sec-fetch-site", "none");
                                                    request.AddHeader("sec-fetch-user", "?1");
                                                    request.AddHeader("upgrade-insecure-requests", "1");
                                                    request.AddHeader("user-agent", ua);
                                                    request.AddHeader("viewport-width", Random.Shared.Next(500, 2000) + "");

                                                    fb_dtsg = Regex.Match(html, @"name=""fb_dtsg"" value=""(.*?)""").Groups[1]
                                                        .Value;
                                                    jazoest = Regex.Match(html, @"name=""jazoest"" value=""(.*?)""").Groups[1]
                                                        .Value;

                                                    var code = await EmailHelper.ReadEmailAsync(email, passEmail);

                                                    requestParams = new RequestParams();
                                                    requestParams["fb_dtsg"] = fb_dtsg;
                                                    requestParams["jazoest"] = jazoest;
                                                    requestParams["code"] = code;
                                                    requestParams["data"] = "ec:0";

                                                    response = request.Post(url, requestParams);

                                                    html = response?.ToString();

                                                    if (html != null)
                                                    {
                                                        // con buoc xac thuc thiet bi nua
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        return string.Empty;
    }

    public static async Task<bool> LikePost(string token)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, "https://graph.facebook.com/100027295904383_1358942941692223/likes?access_token=" + token);
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        if (content != null)
        {
            return bool.Parse(content);
        }

        return false;
    }

    public static bool CheckPoint(string cookie, string ua)
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

            var respone = request.Get("https://www.facebook.com/");

            var url = respone.Address.AbsoluteUri;

            if (url.Contains("checkpoint"))
            {
                return true;
            }

        }
        return false;
    }

    public static async Task<string> GetAccessTokenEaab(string cookie, string ua)
    {
        using (HttpRequest request = new HttpRequest())
        {
            request.AddHeader("authority", "www.facebook.com");
            request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
            request.AddHeader("accept-language", "en-US,en;q=0.9");
            request.AddHeader("cookie", cookie);
            request.AddHeader("dpr", "1");
            request.AddHeader("sec-ch-prefers-color-scheme", "dark");
            request.AddHeader("sec-ch-ua-mobile", "?0");
            request.AddHeader("sec-ch-ua-model", "\"\"");
            request.AddHeader("sec-fetch-dest", "document");
            request.AddHeader("sec-fetch-mode", "navigate");
            request.AddHeader("sec-fetch-site", "none");
            request.AddHeader("sec-fetch-user", "?1");
            request.AddHeader("upgrade-insecure-requests", "1");
            request.AddHeader("user-agent", ua);
            request.AddHeader("viewport-width", Random.Shared.Next(500, 1800) + "");

            var response = request.Get(
                "https://www.facebook.com/dialog/oauth?scope=user_about_me,pages_read_engagement,user_actions.books,user_actions.fitness,user_actions.music,user_actions.news,user_actions.video,user_activities,user_birthday,user_education_history,user_events,user_friends,user_games_activity,user_groups,user_hometown,user_interests,user_likes,user_location,user_managed_groups,user_photos,user_posts,user_relationship_details,user_relationships,user_religion_politics,user_status,user_tagged_places,user_videos,user_website,user_work_history,email,manage_notifications,manage_pages,publish_actions,publish_pages,read_friendlists,read_insights,read_page_mailboxes,read_stream,rsvp_event,read_mailbox&response_type=token&client_id=124024574287414&redirect_uri=fb124024574287414://authorize/&sso_key=com&display=&fbclid=IwAR1KPwp2DVh2Cu7KdeANz-dRC_wYNjjHk5nR5F-BzGGj7-gTnKimAmeg08k");

            string html = response?.ToString();
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "/index1.html", html);
            if (html.Contains("encrypted_post_body"))
            {
                string jazoest = Regex.Match(html, @"name=\\""jazoest\\"" value=\\""(.*?)\\""")?.Groups[1]?.Value;
                string fb_dtsg = Regex.Match(html, @"name=\\""fb_dtsg\\"" value=\\""(.*?)\\""")?.Groups[1]?.Value;
                string scope = Regex.Match(html, @"name=\\""scope\\"" value=\\""(.*?)\\""")?.Groups[1]?.Value;
                string logger_id = Regex.Match(html, @"name=\\""logger_id\\"" value=\\""(.*?)\\""")?.Groups[1]?.Value;
                string encrypted_post_body = Regex.Match(html, @"name=\\""encrypted_post_body\\"" value=\\""(.*?)\\""")?.Groups[1]?.Value;

                request.AddHeader("authority", "www.facebook.com");
                request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                request.AddHeader("accept-language", "en-US,en;q=0.9");
                request.AddHeader("cache-control", "max-age=0");
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("cookie", cookie);
                request.AddHeader("dpr", "1");
                request.AddHeader("origin", "https://www.facebook.com");
                request.AddHeader("referer", "https://www.facebook.com/dialog/oauth?scope=user_about_me%2Cpages_read_engagement%2Cuser_actions.books%2Cuser_actions.fitness%2Cuser_actions.music%2Cuser_actions.news%2Cuser_actions.video%2Cuser_activities%2Cuser_birthday%2Cuser_education_history%2Cuser_events%2Cuser_friends%2Cuser_games_activity%2Cuser_groups%2Cuser_hometown%2Cuser_interests%2Cuser_likes%2Cuser_location%2Cuser_managed_groups%2Cuser_photos%2Cuser_posts%2Cuser_relationship_details%2Cuser_relationships%2Cuser_religion_politics%2Cuser_status%2Cuser_tagged_places%2Cuser_videos%2Cuser_website%2Cuser_work_history%2Cemail%2Cmanage_notifications%2Cmanage_pages%2Cpublish_actions%2Cpublish_pages%2Cread_friendlists%2Cread_insights%2Cread_page_mailboxes%2Cread_stream%2Crsvp_event%2Cread_mailbox&response_type=token&client_id=124024574287414&redirect_uri=fb124024574287414%3A%2F%2Fauthorize%2F&sso_key=com&display=&fbclid=IwAR1KPwp2DVh2Cu7KdeANz-dRC_wYNjjHk5nR5F-BzGGj7-gTnKimAmeg08k");
                request.AddHeader("sec-ch-prefers-color-scheme", "dark");
                request.AddHeader("sec-ch-ua-mobile", "?0");
                request.AddHeader("sec-ch-ua-model", "\"\"");
                request.AddHeader("sec-fetch-dest", "document");
                request.AddHeader("sec-fetch-mode", "navigate");
                request.AddHeader("sec-fetch-site", "same-origin");
                request.AddHeader("sec-fetch-user", "?1");
                request.AddHeader("upgrade-insecure-requests", "1");
                request.AddHeader("user-agent", ua);
                request.AddHeader("viewport-width", Random.Shared.Next(500, 1800) + "");

                var urlParams = new RequestParams();

                urlParams["jazoest"] = jazoest;
                urlParams["fb_dtsg"] = fb_dtsg;
                urlParams["from_post"] = "1";
                urlParams["__CONFIRM__"] = "1";
                urlParams["scope"] = scope;
                urlParams["display"] = "page";
                urlParams["sdk"] = "";
                urlParams["sdk_version"] = "";
                urlParams["domain"] = "";
                urlParams["sso_device"] = "ios";
                urlParams["state"] = "";
                urlParams["user_code"] = "";
                urlParams["nonce"] = "";
                urlParams["logger_id"] = logger_id;
                urlParams["auth_type"] = "";
                urlParams["auth_nonce"] = "";
                urlParams["code_challenge"] = "";
                urlParams["code_challenge_method"] = "";
                urlParams["encrypted_post_body"] = encrypted_post_body;
                urlParams["return_format[]"] = "access_token";

                response = request.Post("https://www.facebook.com/v1.0/dialog/oauth/skip/submit/", urlParams);

                html = response?.ToString();

                Match match = Regex.Match(html, @"#access_token=(.*?)&");
                if (match.Success)
                {
                    return match.Groups[1].Value;
                }
            }
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