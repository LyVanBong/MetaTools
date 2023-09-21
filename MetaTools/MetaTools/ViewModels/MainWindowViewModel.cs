using Prism.Mvvm;

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

        public MainWindowViewModel()
        {
        }
    }
}