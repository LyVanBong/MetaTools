using MetaTools.CookieAndAccessToken.Views;
using Prism.Ioc;
using System.Windows;
using MetaTools.Configurations;

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

        }
    }
}
