namespace MetaTools.Models;

public class AccountErrorModel : AccountModel
{
    public string? ErrorMessage { get; set; }
    public bool IsError { get; set; }
    public override string ToString()
    {
        if (IsError)
            return base.ToString() + "|" + ErrorMessage;
        else
            return base.ToString() + "|" + Cookie;
    }
}