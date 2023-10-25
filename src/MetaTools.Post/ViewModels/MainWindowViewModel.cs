using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace MetaTools.Post.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "MetaTools: Post";
        private string _accessToken;
        private string _uidIdPost;
        private string _pathFile;
        private string _history;
        private readonly IAccountRepository _accountRepository;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string AccessToken
        {
            get => _accessToken;
            set => SetProperty(ref _accessToken, value);
        }

        public string UidIdPost
        {
            get => _uidIdPost;
            set => SetProperty(ref _uidIdPost, value);
        }

        public string PathFile
        {
            get => _pathFile;
            set => SetProperty(ref _pathFile, value);
        }

        public string History
        {
            get => _history;
            set => SetProperty(ref _history, value);
        }

        public ICommand LikePostCommand { get; private set; }
        public ICommand ChooseFileCommand { get; set; }
        public MainWindowViewModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            ChooseFileCommand = new DelegateCommand(() => _ = ChooseFile());
            LikePostCommand = new DelegateCommand(() => _ = LikePost());
        }

        private async Task LikePost()
        {
            var data = await _accountRepository.GetAllAsync();
        }

        private Task ChooseFile()
        {
            throw new System.NotImplementedException();
        }
    }
}
