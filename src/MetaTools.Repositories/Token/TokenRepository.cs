using MetaTools.Models;

namespace MetaTools.Repositories.Token;

public class TokenRepository : ITokenRepository
{
    private readonly SQLiteAsyncConnection _databaseConnection;
    public TokenRepository()
    {
        _databaseConnection = SqLiteSetting.Instance.SqLiteAsyncConnection;
        _databaseConnection.CreateTableAsync<TokenModel>();
    }
    public Task<int> DeleteAsync(string uid)
    {
        return _databaseConnection.DeleteAsync<TokenModel>(uid);
    }

    public Task<int> DeleteAllAsync()
    {
        return _databaseConnection.DeleteAllAsync<TokenModel>();
    }

    public Task<int> AddAsync(TokenModel token)
    {
        return _databaseConnection.InsertOrReplaceAsync(token);
    }

    public Task<int> AddRangeAsync(List<TokenModel> tokens)
    {
        return _databaseConnection.InsertAllAsync(tokens);
    }

    public Task<int> UpdateAsync(TokenModel token)
    {
        return _databaseConnection.UpdateAsync(token);
    }

    public Task<List<TokenModel>> GetAllAsync()
    {
        return _databaseConnection.Table<TokenModel>().ToListAsync();
    }
}