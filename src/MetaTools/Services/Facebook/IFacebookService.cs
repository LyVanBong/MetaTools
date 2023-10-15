namespace MetaTools.Services.Facebook;

public interface IFacebookService
{
    /// <summary>
    /// Get AccessToken EAAB
    /// </summary>
    /// <param name="cookie"></param>
    /// <param name="ua"></param>
    /// <returns></returns>
    string GetAccessTokenEaab(string cookie, string ua);
    /// <summary>
    /// Login facebook get cookie
    /// </summary>
    /// <param name="email"></param>
    /// <param name="pass"></param>
    /// <param name="secretKey"></param>
    /// <param name="ua"></param>
    /// <param name="proxy"></param>
    /// <returns></returns>
    string Login(string email, string pass, string secretKey, string ua, string proxy);
}