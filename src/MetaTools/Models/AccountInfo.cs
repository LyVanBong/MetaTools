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
    private int _sex;
    private string _password;
    private string _secretKey2Fa;
    private string _useragent;
    private string _proxy;
    private int _status;
    private string _descriptions;
    private string _email;
    private string _emailPassword;
    private string _dateCreate;
    private string _dateChange;
    private int _accountType;
    private string _sexText;
    private string _statusText;

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

    public int Sex
    {
        get => _sex;
        set => SetProperty(ref _sex, value, () =>
        {
            if (value == -1)
            {
                SexText = "None";
            }
            else if (value == 0)
            {
                SexText = "Male";
            }
            else if (value == 1)
            {
                SexText = "Female";
            }
        });
    }

    public string SexText
    {
        get => _sexText;
        set => SetProperty(ref _sexText, value);
    }

    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public string SecretKey2Fa
    {
        get => _secretKey2Fa;
        set => SetProperty(ref _secretKey2Fa, value);
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

    public int Status
    {
        get => _status;
        set => SetProperty(ref _status, value, () =>
        {
            switch (value)
            {
                case -1:
                    StatusText = "CheckPoint";
                    break;
                case 0:
                    StatusText = "New";
                    break;
                case 1:
                    StatusText = "Get Cookie";
                    break;
                case 4:
                    StatusText = "Get Cookie Done";
                    break;
                case 2:
                    StatusText = "Get Cookie P5";
                    break;
                case 5:
                    StatusText = "Get Cookie P5 Done";
                    break;
               
                case 3:
                    StatusText = "Live";
                    break;
                default:
                    StatusText = "New";
                    break;
            }
        });
    }

    public string StatusText
    {
        get => _statusText;
        set => SetProperty(ref _statusText, value);
    }

    public string Descriptions
    {
        get => _descriptions;
        set => SetProperty(ref _descriptions, value);
    }
}