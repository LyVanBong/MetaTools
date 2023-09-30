using System.Collections.ObjectModel;
using MetaTools.Core.Mvvm;
using MetaTools.Models;
using Prism.Mvvm;
using Prism.Regions;

namespace MetaTools.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Tools For Meta";

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ObservableCollection<MenuModel> MenuModels { get; set; }
        public MainWindowViewModel()
        {
            MenuModels = new ObservableCollection<MenuModel>()
            {
                new MenuModel()
                {
                    Icon = "../Resources/Images/dashboards.png",
                    Title = "Dashboard",
                    IsActive = true
                },
                new MenuModel()
                {
                    Icon = "../Resources/Images/dashboards.png",
                    Title = "Dashboard",
                },
                new MenuModel()
                {
                    Icon = "../Resources/Images/dashboards.png",
                    Title = "Dashboard",
                }
            };
        }
    }
}