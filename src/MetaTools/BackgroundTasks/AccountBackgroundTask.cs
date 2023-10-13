using MetaTools.Services.Account;

namespace MetaTools.BackgroundTasks;

public class AccountBackgroundTask : BackgroundService
{
    private Timer? _timer = null;
    private readonly IAccountService _accountService;

    public AccountBackgroundTask(IAccountService accountService)
    {
        _accountService = accountService;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(1));
        return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
        _accountService.GetCookieAsync();
    }
}