namespace MetaTools.Services.Facebook;

public interface IFacebookService
{
    /// <summary>
    /// Kiểm tra check point
    /// </summary>
    /// <param name="cookie"></param>
    /// <param name="ua"></param>
    /// <returns></returns>
    (bool CheckPoint, string CheckPointType) CheckPoint(string cookie, string ua);

    /// <summary>
    /// Lấy thông tin tài khoản
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="cookie"></param>
    /// <param name="accessToken"></param>
    /// <param name="ua"></param>
    /// <returns></returns>
    Task<FacebookeInfoModel> GetAccountInfo(string accountId, string cookie, string accessToken, string ua);

    /// <summary>
    /// Get accesstoken eaab2 by insta
    /// </summary>
    /// <param name="cookie"></param>
    /// <param name="ua"></param>
    /// <returns></returns>
    string GetAccessTokenEaab2(string cookie, string ua);

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