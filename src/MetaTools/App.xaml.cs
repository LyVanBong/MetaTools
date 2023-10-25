namespace MetaTools
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _ = MetaToolsConfigurations.RegisterAppCenter();

            // Khởi tạo background service
            var task = Container.Resolve<IBackgroundTaskService>();
            task?.StartAsync(new CancellationToken());
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IUserAgentService, UserAgentService>();
            containerRegistry.RegisterSingleton<IRequestProvider, RequestProvider>();
            containerRegistry.RegisterSingleton<IFacebookService, FacebookService>();
            containerRegistry.RegisterSingleton<ITwoFactorAuthentication, TwoFactorAuthentication>();
            containerRegistry.RegisterSingleton<IBackgroundTaskService, BackgroundTaskService>();
        }
    }
}