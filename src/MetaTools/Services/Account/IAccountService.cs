namespace MetaTools.Services.Account;

public interface IAccountService
{
    /// <summary>
    /// Kiểm tra check point tài khoản
    /// </summary>
    /// <returns></returns>
    Task CheckPoint();
    /// <summary>
    /// Lấy thông tin tài khoản
    /// </summary>
    /// <returns></returns>
    Task GetAccountInfoAsync();
    /// <summary>
    /// Lấy token page eaab
    /// </summary>
    /// <returns></returns>
    Task GetAccessTokenPageEaabAsync();
    /// <summary>
    /// Get token user eaab
    /// </summary>
    /// <returns></returns>
    Task GetAccessTokenUserEaabAsync();
    /// <summary>
    /// Lấy cookie tài khoản
    /// </summary>
    /// <returns></returns>
    Task GetCookieAsync();
    /// <summary>
    /// Thêm tài khoản
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    Task<bool> AddAsync(AccountInfo account);
    /// <summary>
    /// Lấy toàn bộ tài khoản
    /// </summary>
    /// <returns></returns>
    Task<List<AccountInfo>> GetAllAsync();
}