using MetaTools.CookieAndAccessToken.Views;
using Prism.Ioc;
using System.Windows;
using MetaTools.Configurations;
using MetaTools.CookieAndAccessToken.ViewModels;
using MetaTools.Services.Facebook;
using MetaTools.Services.RequestProvider;
using MetaTools.Services.UserAgent;

namespace MetaTools.CookieAndAccessToken
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
            MetaToolsConfigurations.RegisterAppcenter();
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
