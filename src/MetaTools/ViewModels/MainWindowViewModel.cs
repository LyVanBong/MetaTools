using MetaTools.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MetaTools.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Tools For Meta";
        private ObservableCollection<MenuModel> _menuModels;
        private string _moduleTitle = "Dashboard";

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ICommand NavigationCommand { get; private set; }

        public ObservableCollection<MenuModel> MenuModels
        {
            get => _menuModels;
            set => SetProperty(ref _menuModels, value);
        }

        public string ModuleTitle
        {
            get => _moduleTitle;
            set => SetProperty(ref _moduleTitle, value);
        }

        public MainWindowViewModel()
        {
            NavigationCommand = new DelegateCommand<MenuModel>((para) => NavigationAsync(para));
            MenuModels = new ObservableCollection<MenuModel>()
            {
                new MenuModel()
                {
                    IsActive = true,
                    Title = "Dashboard",
                    Icon = "../Resources/Images/dashboards_gray.png",
                    IconWhite = "../Resources/Images/dashboards.png",
                },
                new MenuModel()
                {
                    Title = "Chats",
                    Icon = "../Resources/Images/chats_gray.png",
                    IconWhite = "../Resources/Images/chats.png",
                },
                new MenuModel()
                {
                    Title = "Friends",
                    Icon = "../Resources/Images/friends_gray.png",
                    IconWhite = "../Resources/Images/friends.png",
                },
                new MenuModel()
                {
                    Title = "Project",
                    Icon = "../Resources/Images/project_gray.png",
                    IconWhite = "../Resources/Images/project.png",
                },
                new MenuModel()
                {
                    Title = "Settings",
                    Icon = "../Resources/Images/settings_gray.png",
                    IconWhite = "../Resources/Images/settings.png",
                },
                new MenuModel()
                {
                    Title = "Comments",
                    Icon = "../Resources/Images/comment_gray.png",
                    IconWhite = "../Resources/Images/comment.png",
                }
            };
        }

        private Task NavigationAsync(MenuModel para)
        {
            if (para.IsActive)
                return Task.CompletedTask;

            MenuModels.FirstOrDefault(x => x.IsActive)!.IsActive = false;

            para.IsActive = true;

            ModuleTitle = para.Title;

            return Task.CompletedTask;
        }
    }
}