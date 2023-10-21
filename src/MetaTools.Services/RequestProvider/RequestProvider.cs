namespace MetaTools.Services.RequestProvider;

public class RequestProvider : IRequestProvider
{
    private HttpClient CreateHttpClientt(string proxy = null)
    {
        if (string.IsNullOrEmpty(proxy))
            return new HttpClient();

        WebProxy webProxy = new WebProxy(proxy);
        HttpClientHandler httpClientHandler = new HttpClientHandler()
        {
            Proxy = webProxy,
        };

        return new HttpClient(httpClientHandler);
    }

    private HttpRequestMessage CreateHttpRequestMessage(string url, System.Net.Http.HttpMethod method, List<KeyValuePair<string, string>> headers = null, List<KeyValuePair<string, string>> body = null)
    {
        HttpRequestMessage httpRequestMessage = new HttpRequestMessage(method: method, requestUri: url);
        if (headers is not null)
        {
            foreach (var keyValuePair in headers)
            {
                httpRequestMessage.Headers.Add(keyValuePair.Key, keyValuePair.Value);
            }
        }

        if (body is not null)
        {
            foreach (var keyValuePair in body)
            {
                httpRequestMessage.Content = new System.Net.Http.FormUrlEncodedContent(body);
            }
        }

        return httpRequestMessage;
    }

    public async Task<string> GetAsync(string url, List<KeyValuePair<string, string>> headers = null, List<KeyValuePair<string, string>> body = null, string proxy = null)
    {
        HttpClient client = CreateHttpClientt(proxy);
        HttpRequestMessage httpRequestMessage =
            CreateHttpRequestMessage(url: url, method: System.Net.Http.HttpMethod.Get, headers: headers, body: body);
        var respone = await client.SendAsync(httpRequestMessage);
        respone.EnsureSuccessStatusCode();
        return await respone?.Content?.ReadAsStringAsync();
    }

    public async Task<string> PostAsync(string url, List<KeyValuePair<string, string>> headers = null, List<KeyValuePair<string, string>> body = null, string proxy = null)
    {
        HttpClient client = CreateHttpClientt(proxy);
        HttpRequestMessage httpRequestMessage =
            CreateHttpRequestMessage(url: url, method: System.Net.Http.HttpMethod.Post, headers: headers, body: body);
        var respone = await client.SendAsync(httpRequestMessage);
        respone.EnsureSuccessStatusCode();
        return await respone?.Content?.ReadAsStringAsync();
    }

    public async Task<string> PutAsync(string url, List<KeyValuePair<string, string>> headers = null, List<KeyValuePair<string, string>> body = null, string proxy = null)
    {
        HttpClient client = CreateHttpClientt(proxy);
        HttpRequestMessage httpRequestMessage =
            CreateHttpRequestMessage(url: url, method: System.Net.Http.HttpMethod.Put, headers: headers, body: body);
        var respone = await client.SendAsync(httpRequestMessage);
        respone.EnsureSuccessStatusCode();
        return await respone?.Content?.ReadAsStringAsync();
    }

    public async Task<string> PatchAsync(string url, List<KeyValuePair<string, string>> headers = null, List<KeyValuePair<string, string>> body = null, string proxy = null)
    {
        HttpClient client = CreateHttpClientt(proxy);
        HttpRequestMessage httpRequestMessage =
            CreateHttpRequestMessage(url: url, method: System.Net.Http.HttpMethod.Patch, headers: headers, body: body);
        var respone = await client.SendAsync(httpRequestMessage);
        respone.EnsureSuccessStatusCode();
        return await respone?.Content?.ReadAsStringAsync();
    }

    public async Task<string> DeleteAsync(string url, List<KeyValuePair<string, string>> headers = null, List<KeyValuePair<string, string>> body = null, string proxy = null)
    {
        HttpClient client = CreateHttpClientt(proxy);
        HttpRequestMessage httpRequestMessage =
            CreateHttpRequestMessage(url: url, method: System.Net.Http.HttpMethod.Delete, headers: headers, body: body);
        var respone = await client.SendAsync(httpRequestMessage);
        respone.EnsureSuccessStatusCode();
        return await respone?.Content?.ReadAsStringAsync();
    }

    public async Task<(string Content, CookieContainer Cookie)> GetCookieAsync(string url,
        System.Net.Http.HttpMethod method,
        List<KeyValuePair<string,
            string>> headers = null,
        List<KeyValuePair<string, string>> body = null,
        string proxy = null,
        CookieContainer cookieContainer = null)
    {
        if (cookieContainer == null)
            cookieContainer = new CookieContainer();
        HttpClientHandler httpClientHandler = new HttpClientHandler();
        if (!string.IsNullOrEmpty(proxy))
        {
            httpClientHandler.UseProxy = true;
            httpClientHandler.Proxy = new WebProxy(proxy);
        }

        httpClientHandler.UseCookies = true;
        httpClientHandler.CookieContainer = cookieContainer;
        HttpClient httpClient = new HttpClient(httpClientHandler);
        HttpRequestMessage httpRequestMessage =
            CreateHttpRequestMessage(url: url, method: method, headers: headers, body: body);
        var respone = await httpClient.SendAsync(httpRequestMessage);
        respone.EnsureSuccessStatusCode();
        return (Content: await respone?.Content?.ReadAsStringAsync(), Cookie: cookieContainer);
    }
}