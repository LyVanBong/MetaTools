using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;
using MetaTools.Helpers;
using MetaTools.Services.UserAgent;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.Win32;

namespace MetaTools.CookieAndAccessToken.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "MetaTools: Cookie and AccessToken";
        private bool _isBusy = false;
        private string _loadingText = "Đang tải...";
        private string _pathCookie;
        private string _pathAccount;
        private string _accessToken;
        private string _cookie;
        private string _account;
        private readonly IUserAgentService _userAgentService;
        private string _errorAccount;

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

        public ICommand OpenCheckPointCommand { get; private set; }
        public MainWindowViewModel(IUserAgentService userAgentService)
        {
            _userAgentService = userAgentService;
            GetAccessTokenCommand = new DelegateCommand<string>(s => _ = GetAccessToken(s));
            GetCookieCommand = new DelegateCommand<string>(s => _ = GetCookie(s));
            OpenCheckPointCommand = new DelegateCommand(() => _ = OpenCheckPoint());
        }

        private async Task OpenCheckPoint()
        {
            IsBusy = true;
            if (string.IsNullOrEmpty(ErrorAccount))
            {
                MessageBox.Show("Vui lòng nhập thông tin tài khoản", "Thông báo", MessageBoxButton.OK);
            }
            else
            {
                var data = ErrorAccount.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                if (data != null)
                {
                    await Parallel.ForEachAsync(data, async (s, token) =>
                    {
                        var acc = s.Split('|', StringSplitOptions.RemoveEmptyEntries);
                        if (acc != null)
                        {
                            string uid = acc[0];
                            string pass = acc[1];
                            string code2Fa = acc[2];
                            string email = acc[3];
                            string passEmail = acc[4];
                            string ua = _userAgentService.Generate();
                            LoadingText = "Bắt đầu kiểm tra UID: " + uid;

                            if (FacebookHelper.CheckLogin(uid, pass, code2Fa, ua))
                            {
                                LoadingText = "Tài khoản " + uid + " sai mật khẩu hoặc tài khoản";
                                ErrorAccount = ErrorAccount.Replace(s, s + "|Thông tin đăng nhập sai");
                            }
                            else
                            {
                                var cookie = await FacebookHelper.Login(uid, pass, code2Fa, ua);
                                if (string.IsNullOrEmpty(cookie))
                                {
                                    LoadingText = "Tài khoản " + uid + " lỗi";
                                    ErrorAccount = ErrorAccount.Replace(s, s + "|Thông tin lỗi");
                                }
                                else
                                {
                                    var checkPoint =
                                        await FacebookHelper.CheckPoint_828281030927956(cookie, ua, email, passEmail);
                                    cookie = await FacebookHelper.Login(uid, pass, code2Fa, ua);
                                    if (string.IsNullOrEmpty(cookie))
                                    {
                                        LoadingText = "Tài khoản " + uid + " lỗi";
                                        ErrorAccount = ErrorAccount.Replace(s, s + "|Thông tin lỗi");
                                    }
                                    else
                                    {
                                        Cookie += cookie + "\n";
                                    }
                                }
                            }

                        }

                    });
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
                    openFileDialog.Title = "Chọn file tài khoản";
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
                                      string uid = acc[0];
                                      string pass = acc[1];
                                      string code2Fa = acc[2];
                                      string email = acc[3];
                                      string passEmail = acc[4];
                                      string ua = _userAgentService.Generate();
                                      LoadingText = "Bắt đầu lấy cookie UID: " + uid;

                                      var cookie = await FacebookHelper.Login(uid, pass, code2Fa,
                                          ua);
                                      if (string.IsNullOrEmpty(cookie))
                                      {
                                          ErrorAccount += s + "\n";
                                          Account = Account.Replace(s, "").Replace("\n", "");
                                          LoadingText = "Bắt đầu lấy cookie UID: " + uid + "\nKhông lấy được cookie";
                                      }
                                      else
                                      {
                                          // kiểm tra checkpoint

                                          if (FacebookHelper.CheckPoint(cookie, ua))
                                          {
                                              ErrorAccount += s + "\n";
                                              Account = Account.Replace(s, "").Replace("\n", "");
                                              LoadingText = "Bắt đầu lấy cookie UID: " + uid + "\nTài khoản bị checkpoint";
                                          }
                                          else
                                          {
                                              Cookie = cookie + "\n" + Cookie;
                                              LoadingText = "Bắt đầu lấy cookie UID: " + uid + "\nDone";
                                          }
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
                Analytics.TrackEvent("Get Cookie", new Dictionary<string, string>()
                {
                    {"Key",obj}
                });
                IsBusy = false;
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
                                    var tokenPage = await FacebookHelper.GetTokenPage(token, ua, s);
                                    if (tokenPage != null)
                                    {
                                        foreach (var tk in tokenPage)
                                        {
                                            AccessToken += tk + "\n";
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
            }
        }
    }
}