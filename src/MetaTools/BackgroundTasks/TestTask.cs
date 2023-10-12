using MetaTools.Services.Facebook;

namespace MetaTools.BackgroundTasks;

public class TestTask : BackgroundService
{
    private ILogger<TestTask> _logger;
    private readonly IFacebookService _facebookService;
    public TestTask(ILogger<TestTask> logger, IFacebookService facebookService)
    {
        _logger = logger;
        _facebookService = facebookService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

    }
}