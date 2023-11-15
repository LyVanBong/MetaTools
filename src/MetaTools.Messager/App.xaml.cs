namespace MetaTools.Messager
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

        }
    }
}
