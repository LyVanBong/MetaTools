using MetaTools.Models;

namespace MetaTools.Repositories.Account;

public class AccountRepository : IAccountRepository
{
    private readonly SQLiteAsyncConnection _databaseConnection;
    public AccountRepository()
    {
        _databaseConnection = SqLiteSetting.Instance.SqLiteAsyncConnection;
        _databaseConnection.CreateTableAsync<AccountErrorModel>();
    }
    public Task<int> AddAllAsync(List<AccountErrorModel> accounts)
    {
        return _databaseConnection.InsertAllAsync(accounts);
    }

    public Task<int> AddAsync(AccountErrorModel account)
    {
        return _databaseConnection.InsertOrReplaceAsync(account);
    }

    public Task<int> DeleteAllAsync()
    {
        return _databaseConnection.DeleteAllAsync<AccountErrorModel>();
    }

    public Task<int> DeleteAsync(string uid)
    {
        return _databaseConnection.DeleteAsync<AccountErrorModel>(uid);
    }

    public Task<List<AccountErrorModel>> GetAllAsync()
    {
        return _databaseConnection.Table<AccountErrorModel>().ToListAsync();
    }

    public Task<int> UpdateAsync(AccountErrorModel account)
    {
        return _databaseConnection.UpdateAsync(account);
    }
}