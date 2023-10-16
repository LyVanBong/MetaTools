using MetaTools.Event;
using MetaTools.Services.Facebook;

namespace MetaTools.Services.Account;

public class AccountService : IAccountService
{
    private readonly IAccountInfoRepository _accountInfoRepository;
    private readonly IFacebookService _facebookService;
    private readonly IEventAggregator _eventAggregator;

    public AccountService(IAccountInfoRepository accountInfoRepository, IFacebookService facebookService, IEventAggregator eventAggregator)
    {
        _accountInfoRepository = accountInfoRepository;
        _facebookService = facebookService;
        _eventAggregator = eventAggregator;
    }

    public async Task CheckPoint()
    {
        var acc = await _accountInfoRepository.GetAccountsByStatusAsync(status: 4);
        if (acc != null)
        {
            Analytics.TrackEvent("Check Point", new Dictionary<string, string>() { { "UID", acc.Uid } });
            _ = UpdateAccount(acc, 12);
            var checkPoint = _facebookService.CheckPoint(acc.Cookie, acc.Useragent);
            if (checkPoint.CheckPoint)
            {
                acc.Descriptions = checkPoint.CheckPointType;
                _ = UpdateAccount(acc, -1);
            }
            else
            {
                _ = UpdateAccount(acc, 3);
            }
        }
    }

    public async Task GetAccountInfoAsync()
    {
        var acc = await _accountInfoRepository.GetAccountsByStatusAsync(status: 11);
        if (acc != null)
        {
            Analytics.TrackEvent("Get Account Info", new Dictionary<string, string>() { { "UID", acc.Uid } });

            _ = UpdateAccount(acc, 9);
            var info = await _facebookService.GetAccountInfo(acc.Uid, acc.Cookie, acc.TokenPageEaab, acc.Useragent);
            if (info == null)
            {
                _ = UpdateAccount(acc, -1);
            }
            else
            {
                acc.Name = info.name;
                if (info.gender.ToUpper() == "MALE")
                {
                    acc.Sex = 0;
                }
                else if (info.gender.ToUpper() == "FEMALE")
                {
                    acc.Sex = 1;
                }
                else
                {
                    acc.Sex = -1;
                }

                acc.TotalFriends = info.friends.summary.total_count;
                _ = UpdateAccount(acc, 3);
            }
        }
    }

    public async Task GetAccessTokenPageEaabAsync()
    {
        var acc = await _accountInfoRepository.GetAccountsByStatusAsync(status: 4);
        if (acc != null)
        {
            Analytics.TrackEvent("Get Access Token Page Eaab", new Dictionary<string, string>() { { "UID", acc.Uid } });
            _ = UpdateAccount(acc, 7);
            var token = _facebookService.GetAccessTokenEaab(acc.Cookie, acc.Useragent);
            if (string.IsNullOrEmpty(token))
            {
                _ = UpdateAccount(acc, -1);
            }
            else
            {
                acc.TokenPageEaab = token;
                _ = UpdateAccount(acc, 8);
            }
        }
    }

    public async Task GetAccessTokenUserEaabAsync()
    {
        var acc = await _accountInfoRepository.GetAccountsByStatusAsync(status: 8);
        if (acc != null)
        {
            Analytics.TrackEvent("Get Access Token User Eaab", new Dictionary<string, string>() { { "UID", acc.Uid } });
            _ = UpdateAccount(acc, 10);
            var token = _facebookService.GetAccessTokenEaab2(acc.Cookie, acc.Useragent);
            if (string.IsNullOrEmpty(token))
            {
                _ = UpdateAccount(acc, -1);
            }
            else
            {
                acc.TokenUserEaab = token;
                _ = UpdateAccount(acc, 11);
            }
        }
    }

    private async Task UpdateAccount(AccountInfo acc, int status)
    {
        acc.Status = status;
        acc.DateChange = DateTime.Now.ToString("G");
        await _accountInfoRepository.AddAccountAsync(acc);
        _eventAggregator.GetEvent<UpdateAccountEvent>().Publish(acc);
    }

    public async Task GetCookieAsync()
    {
        var acc = await _accountInfoRepository.GetAccountsByStatusAsync();
        if (acc != null)
        {
            Analytics.TrackEvent("Get Cookie", new Dictionary<string, string>() { { "UID", acc.Uid } });
            _ = UpdateAccount(acc, 1);
            var cookie = _facebookService.Login(acc.Uid, acc.Password, acc.SecretKey2Fa, acc.Useragent, acc.Proxy);
            if (string.IsNullOrEmpty(cookie))
            {
                _ = UpdateAccount(acc, 6);
            }
            else
            {
                acc.Cookie = cookie;
                _ = UpdateAccount(acc, 4);
            }
        }
    }

    public async Task<bool> AddAsync(AccountInfo account)
    {
        try
        {
            return await _accountInfoRepository.AddAccountAsync(account) > 0;
        }
        catch (Exception e)
        {
            Crashes.TrackError(e);
        }

        return false;
    }

    public async Task<List<AccountInfo>> GetAllAsync()
    {
        try
        {
            return await _accountInfoRepository.GetAllAccountsAsync();
        }
        catch (Exception e)
        {
            Crashes.TrackError(e);
        }

        return null;
    }
}