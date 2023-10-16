namespace MetaTools.Services.Account;

public interface IAccountService
{
    /// <summary>
    /// Lấy thông tin tài khoản
    /// </summary>
    /// <returns></returns>
    Task GetAccountInfoAsync();
    /// <summary>
    /// Lấy token eaab
    /// </summary>
    /// <returns></returns>
    Task GetAccessTokenEaabAsync();
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