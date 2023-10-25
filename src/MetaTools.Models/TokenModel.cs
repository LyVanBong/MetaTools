using SQLite;

namespace MetaTools.Models;

public class TokenModel
{
    [PrimaryKey]
    public string Uid { get; set; }
    public string Token { get; set; }
}