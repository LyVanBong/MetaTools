using System.Linq;
using MetaTools.Helpers;

namespace MetaTools.Post.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "MetaTools: Post";
        private string _accessToken = string.Empty;
        private string _uidIdPost = string.Empty;
        private string _pathFile = string.Empty;
        private string _history = string.Empty;
        private readonly ITokenRepository _tokenRepository;
        private string _loadingText = "Đang tải...";
        private bool _isBusy;

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

        public string LoadingText
        {
            get => _loadingText;
            set => SetProperty(ref _loadingText, value);
        }

        public ICommand LikePostCommand { get; private set; }
        public ICommand ChooseFileCommand { get; set; }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public MainWindowViewModel(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
            ChooseFileCommand = new DelegateCommand(() => _ = ChooseFile());
            LikePostCommand = new DelegateCommand(() => _ = LikePost());
            _ = InitData();
        }

        private async Task InitData()
        {
            IsBusy = true;
            var data = await _tokenRepository.GetAllAsync();
            if (data.Any())
            {
                foreach (var t in data)
                {
                    AccessToken += t.Token + "\n";
                }
            }
            IsBusy = false;
        }

        private async Task LikePost()
        {
            IsBusy = true;
            try
            {
                if (string.IsNullOrEmpty(AccessToken) || string.IsNullOrEmpty(UidIdPost))
                {
                    MessageBox.Show("Nhập đủ thông tin", "Thông báo", MessageBoxButton.OK);
                }
                else
                {
                    var listId = UidIdPost.Split("\n", StringSplitOptions.RemoveEmptyEntries);
                    var listToken = AccessToken.Split("\n", StringSplitOptions.RemoveEmptyEntries);
                    foreach (var id in listId)
                    {
                        foreach (var tk in listToken)
                        {
                            var like = await FacebookHelper.LikePost(token: tk, idUserIdPost: id);
                            History += id.Trim() + "|" + tk.Trim() + "|" + (like ? "Thành công" : "Lỗi") + "\n";
                            await Task.Delay(Random.Shared.Next(1000, 3000));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ChooseFile()
        {
            IsBusy = true;
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.InitialDirectory =
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openFileDialog.Title = "Chọn tệp tin token";
                if (openFileDialog.ShowDialog() == true)
                {
                    PathFile = openFileDialog.FileName;
                    AccessToken += await File.ReadAllTextAsync(path: PathFile);
                }
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
