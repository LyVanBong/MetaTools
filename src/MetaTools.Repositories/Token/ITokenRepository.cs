using MetaTools.Models;

namespace MetaTools.Repositories.Token;

public interface ITokenRepository
{
    Task<int> DeleteAsync(string uid);
    Task<int> DeleteAllAsync();
    Task<int> AddAsync(TokenModel token);
    Task<int> AddRangeAsync(List<TokenModel> tokens);
    Task<int> UpdateAsync(TokenModel token);
    Task<List<TokenModel>> GetAllAsync();
}