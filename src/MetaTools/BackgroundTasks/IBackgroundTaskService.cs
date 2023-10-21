namespace MetaTools.BackgroundTasks;

public interface IBackgroundTaskService : IDisposable
{
    Task StartAsync(CancellationToken stoppingToken);

    Task StopAsync(CancellationToken stoppingToken);
}