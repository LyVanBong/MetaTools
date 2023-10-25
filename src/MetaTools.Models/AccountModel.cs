using SQLite;

namespace MetaTools.Models;

public class AccountModel
{
    [PrimaryKey]
    public string? Uid { get; set; }
    public string? Pass { get; set; }
    public string? Code2Fa { get; set; }
    public string? Email { get; set; }
    public string? PassEmail { get; set; }
    public string? UserAgent { get; set; }
    public string? Cookie { get; set; }
    public string? Token { get; set; }
    public override string ToString()
    {
        return Uid + "|" + Pass + "|" + Code2Fa + "|" + Email + "|" + PassEmail + "|" + UserAgent + "|" + Cookie + "|" +
               Token;
    }
}