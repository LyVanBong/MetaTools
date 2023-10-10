namespace MetaTools.Services.UserAgent;

public interface IUserAgentService
{
    Task<UserAgentModel> Generate();
}