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
        string ua = "Mozilla/5.0 (X11; Linux i686; rv:49.0) Gecko/20100101 Firefox/49.0";
        var para = await _facebookService.GetParaLogin(ua);

        var data = await _facebookService.Login(para.action, "bonglvno1@gmail.com", "D1fc0nku.,@123", ua, para.lsd,
            para.jazoest, para.m_ts, para.li, para.try_number, para.unrecognized_tries, para.login, para.bi_xrwh);

    }
}