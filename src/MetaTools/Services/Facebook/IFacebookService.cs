namespace MetaTools.Services.Facebook;

public interface IFacebookService
{
    Task<string> Login(string email, string pass, string secretKey, string ua, string proxy);
}