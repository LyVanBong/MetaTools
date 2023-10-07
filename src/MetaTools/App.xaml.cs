using MetaTools.ViewModels;
using MetaTools.Views;
using Prism.Ioc;
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
            containerRegistry.RegisterForNavigation<CommentView, CommentViewModel>();
        }
    }
}