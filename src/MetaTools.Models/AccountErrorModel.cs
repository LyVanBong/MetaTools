namespace MetaTools.Models;

public class AccountErrorModel : AccountModel
{
    public string? ErrorMessage { get; set; }
    public bool IsError { get; set; }
    public override string ToString()
    {
        return (base.ToString() + "|" + IsError + "|" + ErrorMessage).Replace("||", "|");
    }
}