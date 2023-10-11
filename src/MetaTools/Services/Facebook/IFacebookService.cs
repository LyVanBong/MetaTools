namespace MetaTools.Services.Facebook;

public interface IFacebookService
{
    Task<(string action, string lsd, string jazoest, string m_ts, string li, string login, string bi_xrwh, string unrecognized_tries, string try_number)> GetParaLogin(string ua);

    Task<string> Login(string action, string email, string pass, string ua, string lsd, string jazoest, string m_ts, string li, string try_number, string unrecognized_tries, string login, string bi_xrwh);
}