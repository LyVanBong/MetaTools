using MetaTools.Models;

namespace MetaTools.CookieToken.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "MetaTools: Cookie and AccessToken";
        private bool _isBusy = false;
        private string _loadingText = "Đang tải...";
        private string _pathCookie;
        private string _pathAccount;
        private string _accessToken = string.Empty;
        private string _cookie = string.Empty;
        private string _account;
        private readonly IUserAgentService _userAgentService;
        private string _errorAccount = string.Empty;
        private readonly ITokenRepository _tokenRepository;
        private readonly IAccountRepository _accountRepository;
        private List<TokenModel> _tokenModels = new List<TokenModel>();
        private List<AccountErrorModel> _accountModels = new List<AccountErrorModel>();
        public string ErrorAccount
        {
            get => _errorAccount;
            set => SetProperty(ref _errorAccount, value);
        }

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

        public string LoadingText
        {
            get => _loadingText;
            set => SetProperty(ref _loadingText, value);
        }

        public string PathCookie
        {
            get => _pathCookie;
            set => SetProperty(ref _pathCookie, value);
        }

        public ICommand GetAccessTokenCommand { get; private set; }

        public string PathAccount
        {
            get => _pathAccount;
            set => SetProperty(ref _pathAccount, value);
        }

        public ICommand GetCookieCommand { get; private set; }

        public string AccessToken
        {
            get => _accessToken;
            set => SetProperty(ref _accessToken, value);
        }

        public string Account
        {
            get => _account;
            set => SetProperty(ref _account, value);
        }

        public string Cookie
        {
            get => _cookie;
            set => SetProperty(ref _cookie, value);
        }

        public ICommand DeleteDataCommand { get; private set; }
        public MainWindowViewModel(IUserAgentService userAgentService, ITokenRepository tokenRepository, IAccountRepository accountRepository)
        {
            _userAgentService = userAgentService;
            _tokenRepository = tokenRepository;
            _accountRepository = accountRepository;
            GetAccessTokenCommand = new DelegateCommand<string>(s => _ = GetAccessToken(s));
            GetCookieCommand = new DelegateCommand<string>(s => _ = GetCookie(s));
            DeleteDataCommand = new DelegateCommand<string>((key) => _ = DeleteData(key));
            _ = InitData();
        }

        private async Task DeleteData(string key)
        {
            IsBusy = true;
            try
            {
                if (key == "0")
                {
                    var deleteAccount = await _accountRepository.DeleteAllAsync();
                    if (deleteAccount > 0)
                    {
                        Account = string.Empty;
                        ErrorAccount = string.Empty;
                        Cookie = string.Empty;
                    }
                }
                else
                {
                    var deleteToken = await _tokenRepository.DeleteAllAsync();
                    if (deleteToken > 0)
                    {
                        AccessToken = string.Empty;
                    }
                }
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
            }

            IsBusy = false;
        }

        private async Task InitData()
        {
            IsBusy = true;
            await Task.Delay(1000);

            _tokenModels = await _tokenRepository.GetAllAsync();

            if (_tokenModels != null && _tokenModels.Any())
            {
                foreach (var token in _tokenModels)
                {
                    AccessToken += token.Token + "\n";
                }
            }

            _accountModels = await _accountRepository.GetAllAsync();
            if (_accountModels != null && _accountModels.Any())
            {
                foreach (var account in _accountModels)
                {
                    if (account.IsError)
                    {
                        ErrorAccount += account.ToString() + "\n";
                    }
                    else
                    {
                        Account += account.ToString() + "\n";
                        Cookie += account.Cookie + "\n";
                    }
                }
            }

            IsBusy = false;
        }

        private async Task GetCookie(string obj)
        {
            try
            {
                IsBusy = true;
                if (obj == "1")
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog.InitialDirectory =
                        Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    openFileDialog.Title = "Chọn tệp tin tài khoản";
                    if (openFileDialog.ShowDialog() == true)
                    {
                        PathAccount = openFileDialog.FileName;
                        Account += await File.ReadAllTextAsync(PathAccount);
                    }
                }
                else if (obj == "0")
                {
                    if (string.IsNullOrEmpty(Account))
                    {
                        MessageBox.Show("Vui lòng nhập thông tin tài khoản", "Thông báo", MessageBoxButton.OK);
                    }
                    else
                    {
                        var data = Account.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                        if (data != null)
                        {
                            await Parallel.ForEachAsync(data, async (s, token) =>
                              {
                                  var acc = s.Split('|', StringSplitOptions.RemoveEmptyEntries);
                                  if (acc != null)
                                  {
                                      var a = _accountModels.FirstOrDefault(x => x.Uid == acc[0]);

                                      var account = new AccountErrorModel()
                                      {
                                          Uid = acc[0],
                                          Pass = acc[1],
                                          Code2Fa = acc[2],
                                          Email = acc[3],
                                          PassEmail = acc[4],
                                          UserAgent = (a == null ? _userAgentService.Generate() : a.UserAgent)?.Trim(),
                                      };

                                      LoadingText = "Bắt đầu lấy cookie UID: " + account.Uid;

                                      Cookie = Cookie.Replace(s, account.ToString().Trim());

                                      var cookie = await FacebookHelper.LoginMFacebook(account.Uid, account.Pass, account.Code2Fa,
                                          account!.UserAgent);
                                      if (string.IsNullOrEmpty(cookie))
                                      {
                                          account.IsError = true;
                                          account.ErrorMessage = "không get được cookie)";
                                          Account = Account.Replace(s, "").Replace("\n", "");
                                          ErrorAccount += account.ToString() + "\n";

                                          LoadingText = "Bắt đầu lấy cookie UID: " + account.Uid + "\nKhông lấy được cookie";
                                      }
                                      else
                                      {
                                          account.Cookie = cookie;
                                          // kiểm tra checkpoint
                                          var checkPoint =await FacebookHelper.CheckPoint(account);

                                          if (string.IsNullOrEmpty(checkPoint))
                                          {
                                              account.IsError = true;
                                              account.ErrorMessage = "không get được cookie)";
                                              Account = Account.Replace(s, "").Replace("\n", "");
                                              ErrorAccount += account.ToString() + "\n";
                                          }
                                          else
                                          {
                                              Cookie += checkPoint + "\n";
                                          }
                                      }

                                      await _accountRepository.AddAsync(account);
                                  }
                              });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
                MessageBox.Show("Lỗi: " + e.Message);
            }
            finally
            {
                Analytics.TrackEvent("Get Cookie", new Dictionary<string, string>()
                {
                    {"Key",obj}
                });
                IsBusy = false;
                LoadingText = "Đang tải...";
            }
        }

        private async Task GetAccessToken(string obj)
        {
            try
            {
                IsBusy = true;
                if (obj == "1")
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog.InitialDirectory =
                        Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    openFileDialog.Title = "Chọn file cookie";
                    if (openFileDialog.ShowDialog() == true)
                    {
                        PathCookie = openFileDialog.FileName;
                        Cookie += await File.ReadAllTextAsync(PathCookie);
                    }
                }
                else if (obj == "0")
                {
                    if (string.IsNullOrEmpty(Cookie))
                    {
                        MessageBox.Show("Vui lòng nhập thông tin cookie", "Thông báo", MessageBoxButton.OK);
                    }
                    else
                    {
                        var data = Cookie.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                        if (data != null)
                        {
                            await Parallel.ForEachAsync(data, async (s, cancel) =>
                            {
                                var u = s.Split("c_user=")[1].Split(";")[0];

                                LoadingText = "Get token cookie: " + s;
                                var ua = _userAgentService.Generate();
                                var token = await FacebookHelper.GetAccessTokenEaab(s, ua);
                                if (string.IsNullOrEmpty(token))
                                {
                                    LoadingText = "Get token cookie: " + s + "\n Không lấy được accesstoken từ tài khoản này";
                                }
                                else
                                {
                                    AccessToken += token + "\n";
                                    await _tokenRepository.AddAsync(new TokenModel()
                                    {
                                        Uid = u,
                                        Token = token
                                    });
                                    var tokenPage = await FacebookHelper.GetTokenPage(token, ua, s);
                                    if (tokenPage != null)
                                    {
                                        int i = 2;
                                        foreach (var tk in tokenPage)
                                        {
                                            AccessToken += tk + "\n";
                                            await _tokenRepository.AddAsync(new TokenModel()
                                            {
                                                Uid = u + "_" + i,
                                                Token = token
                                            });
                                            i++;
                                        }
                                    }
                                    else
                                    {
                                        LoadingText = "Get token cookie: " + s + "\nTài khoản này không có token page";
                                    }
                                }
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
                MessageBox.Show("Lỗi: " + e.Message);
            }
            finally
            {
                Analytics.TrackEvent("Get AccessToken", new Dictionary<string, string>()
                {
                    {"Key",obj}
                });
                IsBusy = false;
                LoadingText = "Đang tải...";
            }
        }
    }
}