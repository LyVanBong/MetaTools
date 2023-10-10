namespace MetaTools.Models;

public class AccountInfo : BindableBase
{
    private bool _isSeclected;
    private string _uid;
    private string _name;
    private string _token;
    private string _cookie;
    private int _totalFriends;
    private int _totalGroups;
    private int _totalPages;
    private string _birthday;
    private string _sex;
    private string _password;
    private string _twoFaCode;
    private string _useragent;
    private string _proxy;
    private string _status = "New";
    private string _descriptions;
    private string _email;
    private string _emailPassword;
    private string _dateCreate;
    private string _dateChange;
    private int _accountType;

    public int AccountType
    {
        get => _accountType;
        set => SetProperty(ref _accountType, value);
    }

    public string DateChange
    {
        get => _dateChange;
        set => SetProperty(ref _dateChange, value);
    }

    public string DateCreate
    {
        get => _dateCreate;
        set => SetProperty(ref _dateCreate, value);
    }

    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    public string EmailPassword
    {
        get => _emailPassword;
        set => SetProperty(ref _emailPassword, value);
    }

    public bool IsSeclected
    {
        get => _isSeclected;
        set => SetProperty(ref _isSeclected, value);
    }

    [PrimaryKey]
    public string Uid
    {
        get => _uid;
        set => SetProperty(ref _uid, value);
    }

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public string Token
    {
        get => _token;
        set => SetProperty(ref _token, value);
    }

    public string Cookie
    {
        get => _cookie;
        set => SetProperty(ref _cookie, value);
    }

    public int TotalFriends
    {
        get => _totalFriends;
        set => SetProperty(ref _totalFriends, value);
    }

    public int TotalGroups
    {
        get => _totalGroups;
        set => SetProperty(ref _totalGroups, value);
    }

    public int TotalPages
    {
        get => _totalPages;
        set => SetProperty(ref _totalPages, value);
    }

    public string Birthday
    {
        get => _birthday;
        set => SetProperty(ref _birthday, value);
    }

    public string Sex
    {
        get => _sex;
        set => SetProperty(ref _sex, value);
    }

    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public string TwoFaCode
    {
        get => _twoFaCode;
        set => SetProperty(ref _twoFaCode, value);
    }

    public string Useragent
    {
        get => _useragent;
        set => SetProperty(ref _useragent, value);
    }

    public string Proxy
    {
        get => _proxy;
        set => SetProperty(ref _proxy, value);
    }

    public string Status
    {
        get => _status;
        set => SetProperty(ref _status, value);
    }

    public string Descriptions
    {
        get => _descriptions;
        set => SetProperty(ref _descriptions, value);
    }
}