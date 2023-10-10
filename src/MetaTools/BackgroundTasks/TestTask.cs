namespace MetaTools.BackgroundTasks;

public class TestTask : BackgroundService
{
    private ILogger<TestTask> _logger;

    public TestTask(ILogger<TestTask> logger)
    {
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}