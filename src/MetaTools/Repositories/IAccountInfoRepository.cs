namespace MetaTools.Repositories;

public interface IAccountInfoRepository
{
    Task<AccountInfo> GetAccountsByStatusAsync(int status = 0);
    Task<int> AddAccountAsync(AccountInfo account);

    Task<int> AddAccountsAsync(IEnumerable<AccountInfo> accounts);

    Task<AccountInfo> GetAccountAsync(string uid);

    Task<List<AccountInfo>> GetAllAccountsAsync();

    Task<int> DeleteAccount(string uid);

    Task<int> DeleteAllAccount();
}