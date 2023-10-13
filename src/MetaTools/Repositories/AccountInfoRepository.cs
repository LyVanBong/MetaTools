namespace MetaTools.Repositories;

public class AccountInfoRepository : IAccountInfoRepository
{
    private SQLiteAsyncConnection _database;

    public AccountInfoRepository()
    {
        SQLite.SQLiteOpenFlags flags =
           // open the database in read/write mode
           SQLite.SQLiteOpenFlags.ReadWrite |
           // create the database if it doesn't exist
           SQLite.SQLiteOpenFlags.Create |
           // enable multi-threaded database access
           SQLite.SQLiteOpenFlags.SharedCache;

        string databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "54bced1dbc5dcf5813f154e3e58141e6.db");

        string databaseKey = "3e46372e01b0d19ab5c21ab96a8200bae5726bd6af5d9cd85dff62096daa083d2fc23693ccedc6556b815766d24ae2adec632bcae85ffd13014057a8d30d3fe0";
        var options = new SQLiteConnectionString(databasePath: databasePath,
                    openFlags: flags,
                    key: databaseKey,
                    storeDateTimeAsTicks: true);
        _database = new SQLiteAsyncConnection(options);
        _database.CreateTableAsync<AccountInfo>();
        _database.EnableWriteAheadLoggingAsync();
    }

    public Task<AccountInfo> GetAccountNewAsync()
    {
        return _database.FindAsync<AccountInfo>(x => x.Status == 0);
    }

    public Task<int> AddAccountAsync(AccountInfo account)
    {
        return _database.InsertOrReplaceAsync(account);
    }

    public Task<int> AddAccountsAsync(IEnumerable<AccountInfo> accounts)
    {
        return _database.InsertAllAsync(accounts);
    }

    public Task<AccountInfo> GetAccountAsync(string uid)
    {
        return _database.FindAsync<AccountInfo>(x => x.Uid == uid);
    }

    public Task<List<AccountInfo>> GetAllAccountsAsync()
    {
        return _database.Table<AccountInfo>().ToListAsync();
    }

    public Task<int> DeleteAccount(string uid)
    {
        return _database.DeleteAsync<AccountInfo>(uid);
    }

    public Task<int> DeleteAllAccount()
    {
        return _database.DeleteAllAsync<AccountInfo>();
    }
}