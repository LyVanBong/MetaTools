using Prism.Mvvm;

namespace MetaTools.CookieAndAccessToken.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "MetaTools: Cookie and AccessToken";
        private bool _isBusy = false;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public MainWindowViewModel()
        {

        }
    }
}
