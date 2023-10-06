using MetaTools.Modules.Comments.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MetaTools.Modules.Comments.ViewModels
{
    public class CommentViewModel : BindableBase
    {
        private string _cookies;
        private string _posts;
        private string _userAgent;
        private string _logs;

        public string UserAgent
        {
            get => _userAgent;
            set => SetProperty(ref _userAgent, value);
        }

        public string Posts
        {
            get => _posts;
            set => SetProperty(ref _posts, value);
        }

        public string Cookies
        {
            get => _cookies;
            set => SetProperty(ref _cookies, value);
        }

        public string Logs
        {
            get => _logs;
            set => SetProperty(ref _logs, value);
        }

        public ICommand StartCommand { get; private set; }
        public ICommand StopCommand { get; private set; }

        public CommentViewModel()
        {
            StartCommand = new DelegateCommand(StartAsync);
            StopCommand = new DelegateCommand(StopAsync);
        }

        private void StopAsync()
        {
            Logger("Tính năng stop chưa hoạt động");
        }

        private async void StartAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(Cookies) || string.IsNullOrEmpty(UserAgent) || string.IsNullOrEmpty(Posts))
                {
                    MessageBox.Show("Cập nhật đầy đủ thông tin !");
                    Logger("Thông tin nhập thiếu");
                    return;
                }

                Logger("Bắt đầu chạy tool");
                var listCookies = Cookies.Split("\n", StringSplitOptions.RemoveEmptyEntries);
                Logger("Cookie ok");
                var listUserAgent = UserAgent.Split("\n", StringSplitOptions.RemoveEmptyEntries);
                Logger("User agent ok");

                await BuffLikeComment(listUserAgent, listCookies);

                Logger("Buff done");
                Posts = string.Empty;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
                Logger(e.StackTrace);
            }
        }

        private async Task BuffLikeComment(string[] listUserAgent, string[] listCookies)
        {
            int lenUa = listUserAgent.Length - 1;
            foreach (var cookie in listCookies)
            {
                var ck = cookie.Replace(" ", "").Trim();
                Logger("Lọc ký tự thừa trong cookie");
                var uid = ck.Split("c_user=")[1].Split(';')[0];
                Logger("Bắt đầu với UID: " + uid);

                var ua = listUserAgent[lenUa];
                var likeComment = await FacebookHelper.GetLinkLikeComment(ck, ua, Posts);
                if (string.IsNullOrEmpty(likeComment))
                {
                    Logger("Không lấy được link like comment");
                }
                else
                {
                    await FacebookHelper.BuffLikeComment(ck, likeComment, ua);
                    Logger("Like comment xong băng UID: " + uid);
                }

                await Task.Delay(Random.Shared.Next(1000, 3000));

                if (lenUa == 0)
                {
                    lenUa = listUserAgent.Length - 1;
                }
            }

            MessageBox.Show("Buff like comment done");
        }

        private void Logger(string log)
        {
            Logs = $"[{DateTime.Now}]: " + log + "\n" + Logs;
        }
    }
}