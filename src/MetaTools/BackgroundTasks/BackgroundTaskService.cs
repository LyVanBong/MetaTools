namespace MetaTools.BackgroundTasks;

public class BackgroundTaskService : IBackgroundTaskService
{
    private Timer _timer = null;

    public BackgroundTaskService()
    {
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