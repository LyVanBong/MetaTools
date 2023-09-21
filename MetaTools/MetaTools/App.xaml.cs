using MetaTools.Modules.AddFriends;
using MetaTools.Modules.ModuleName;
using MetaTools.Services;
using MetaTools.Services.Interfaces;
using MetaTools.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

namespace MetaTools
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<AddFriendsModule>();
            moduleCatalog.AddModule<ModuleNameModule>();
        }
    }
}