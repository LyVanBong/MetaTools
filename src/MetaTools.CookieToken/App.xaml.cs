namespace MetaTools.CookieToken
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            MetaToolsConfigurations.RegisterSyncfusion();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _ = MetaToolsConfigurations.RegisterAppCenter();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>();

            containerRegistry.RegisterSingleton<IFacebookService, FacebookService>();
            containerRegistry.RegisterSingleton<IUserAgentService, UserAgentService>();
            containerRegistry.RegisterSingleton<IRequestProvider, RequestProvider>();
        }
    }
}