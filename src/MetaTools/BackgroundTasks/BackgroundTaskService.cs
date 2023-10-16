namespace MetaTools.BackgroundTasks;

public class BackgroundTaskService : IBackgroundTaskService
{
    private Timer _timer = null;
    private readonly IAccountService _accountService;

    public BackgroundTaskService(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(2));

        return Task.CompletedTask;
    }
    private void DoWork(object? state)
    {
        Debug.WriteLine("DoWork: " + DateTime.Now);

        _accountService.GetCookieAsync();

        _accountService.CheckPoint();

        //_accountService.GetAccessTokenPageEaabAsync();

        //_accountService.GetAccessTokenUserEaabAsync();

        //_accountService.GetAccountInfoAsync();
    }
    public Task StopAsync(CancellationToken stoppingToken)
    {
        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}