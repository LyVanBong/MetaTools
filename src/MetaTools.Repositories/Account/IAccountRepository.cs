using MetaTools.Models;

namespace MetaTools.Repositories.Account;

public interface IAccountRepository
{
    Task<int> AddAsync(AccountErrorModel account);
    Task<int> AddAllAsync(List<AccountErrorModel> accounts);
    Task<int> UpdateAsync(AccountErrorModel account);
    Task<int> DeleteAsync(string uid);
    Task<int> DeleteAllAsync();
    Task<List<AccountErrorModel>> GetAllAsync();
}