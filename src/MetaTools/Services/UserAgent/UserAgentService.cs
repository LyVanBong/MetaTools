namespace MetaTools.Services.UserAgent;

public class UserAgentService : IUserAgentService
{
    private readonly IRequestProvider _requestProvider;

    public UserAgentService(IRequestProvider requestProvider)
    {
        _requestProvider = requestProvider;
    }

    public async Task<UserAgentModel> Generate()
    {
        try
        {
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("apikey","l7L8YsSTg3QahKMXFHFgJWI2w35j9YdY")
            };
            var data = await _requestProvider.GetAsync("https://api.apilayer.com/user_agent/generate", headers: headers);

            return JsonSerializer.Deserialize<UserAgentModel>(data);
        }
        catch (Exception e)
        {
            Crashes.TrackError(e);
            return null;
        }
    }
}