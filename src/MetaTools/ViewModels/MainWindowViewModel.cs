using MetaTools.Models;
using MetaTools.Views;
using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MetaTools.ViewModels
{
    public class MainWindowViewModel : RegionViewModelBase
    {
        private string _title = "Tools For Meta";

        private ObservableCollection<MenuModel> _menuModels = new ObservableCollection<MenuModel>()
        {
            new MenuModel()
            {
                Id = 0,
                IsActive = true,
                Title = "Dashboard",
                Icon = "../Resources/Images/dashboards_gray.png",
                IconWhite = "../Resources/Images/dashboards.png",
                ContentRegion = nameof(DashboardView)

            },
            new MenuModel()
            {
                Id = 1,
                Title = "Comments",
                Icon = "../Resources/Images/comment_gray.png",
                IconWhite = "../Resources/Images/comment.png",
                ContentRegion = nameof(CommentView)
            },
            new MenuModel()
            {
                Id = 2,
                Title = "Chats",
                Icon = "../Resources/Images/chats_gray.png",
                IconWhite = "../Resources/Images/chats.png",
            },
            new MenuModel()
            {
                Id = 3,
                Title = "Friends",
                Icon = "../Resources/Images/friends_gray.png",
                IconWhite = "../Resources/Images/friends.png",
            },
            new MenuModel()
            {
                Id = 4,
                Title = "Accounts",
                Icon = "../Resources/Images/account_gray.png",
                IconWhite = "../Resources/Images/account.png",
                ContentRegion = nameof(AccountsView)
            },
            new MenuModel()
            {
                Id = 5,
                Title = "Settings",
                Icon = "../Resources/Images/settings_gray.png",
                IconWhite = "../Resources/Images/settings.png",
            },

        };

        private string _moduleTitle = "Dashboard";
        private readonly IDialogService _dialogService;
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

        public MainWindowViewModel(IRegionManager regionManager, IDialogService dialogService) : base(regionManager)
        {
            _dialogService = dialogService;
            NavigationCommand = new DelegateCommand<MenuModel>((para) => NavigationAsync(para));
            _ = Initialization();
        }

        private async Task Initialization()
        {
            await Task.Delay(1000);
            RegionManager.RequestNavigate("ContentRegion", nameof(DashboardView));
        }


        private Task NavigationAsync(MenuModel para)
        {
            if (para.IsActive)
                return Task.CompletedTask;

            MenuModels.FirstOrDefault(x => x.IsActive)!.IsActive = false;

            para.IsActive = true;

            ModuleTitle = para.Title;
            if (para.ContentRegion != null)
            {
                RegionManager.RequestNavigate("ContentRegion", para.ContentRegion);
            }

            return Task.CompletedTask;
        }
    }
}