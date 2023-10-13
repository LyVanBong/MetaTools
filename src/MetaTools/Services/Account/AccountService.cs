using MetaTools.Services.Facebook;

namespace MetaTools.Services.Account;

public class AccountService : IAccountService
{
    private readonly IAccountInfoRepository _accountInfoRepository;
    private readonly IFacebookService _facebookService;

    public AccountService(IAccountInfoRepository accountInfoRepository, IFacebookService facebookService)
    {
        _accountInfoRepository = accountInfoRepository;
        _facebookService = facebookService;
    }

    public async Task GetCookieAsync()
    {
        var acc = await _accountInfoRepository.GetAccountNewAsync();
        if (acc != null)
        {
            acc.Status = 1;
            acc.DateChange = DateTime.Now.ToString("G");
            await _accountInfoRepository.AddAccountAsync(acc);
            var cookie = await _facebookService.Login(acc.Email, acc.Password, acc.SecretKey2Fa, acc.Useragent, acc.Proxy);
            if (string.IsNullOrEmpty(cookie))
            {
                acc.Status = 0;
                acc.DateChange = DateTime.Now.ToString("G");
                await _accountInfoRepository.AddAccountAsync(acc);
            }
            else
            {
                acc.Cookie = cookie;
                acc.DateChange = DateTime.Now.ToString("G");
                await _accountInfoRepository.AddAccountAsync(acc);
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