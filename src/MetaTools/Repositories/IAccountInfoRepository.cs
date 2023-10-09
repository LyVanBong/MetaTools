using System.Collections.Generic;
using System.Threading.Tasks;
using MetaTools.Models;

namespace MetaTools.Repositories;

public interface IAccountInfoRepository
{
    Task<int> AddAccountAsync(AccountInfo account);
    Task<int> AddAccountsAsync(IEnumerable<AccountInfo> accounts);
    Task<AccountInfo> GetAccountAsync(string uid);
    Task<List<AccountInfo>> GetAllAccountsAsync();
    Task<int> DeleteAccount(string uid);
    Task<int> DeleteAllAccount();
}