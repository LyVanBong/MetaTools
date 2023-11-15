using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using MetaTools.Messager.Models;
using Microsoft.AppCenter.Crashes;
using Prism.Commands;
using Prism.Mvvm;

namespace MetaTools.Messager.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Send Messager";
        private string _accessToken = string.Empty;
        private string _friends = string.Empty;
        private string _messages = string.Empty;
        private string _loadingText = "Đang tải...";
        private bool _isBusy;
        private List<Datum> _listId = new List<Datum>();
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ICommand StartSendingMessagesCommand { get; private set; }

        public string AccessToken
        {
            get => _accessToken;
            set => SetProperty(ref _accessToken, value);
        }

        public string Friends
        {
            get => _friends;
            set => SetProperty(ref _friends, value);
        }

        public string Messages
        {
            get => _messages;
            set => SetProperty(ref _messages, value);
        }

        public string LoadingText
        {
            get => _loadingText;
            set => SetProperty(ref _loadingText, value);
        }

        public MainWindowViewModel()
        {
            StartSendingMessagesCommand = new DelegateCommand(() => _ = StartSendingMessagesAsync());
        }

        private async Task StartSendingMessagesAsync()
        {
            IsBusy = true;
            try
            {
                if (string.IsNullOrEmpty(AccessToken) || string.IsNullOrEmpty(Messages))
                {
                    MessageBox.Show("Nhập thiếu thông tin!", "Thông báo", MessageBoxButton.OK);
                }
                else
                {
                    LoadingText = "Bắt đầu kiểm tra bạn bè";

                    string url =
                        "https://graph.facebook.com/v18.0/me/conversations?limit=499&access_token=" + AccessToken;
                    while (true)
                    {
                        var client = new HttpClient();
                        var request = new HttpRequestMessage(HttpMethod.Get, url);
                        var response = await client.SendAsync(request);
                        response.EnsureSuccessStatusCode();
                        var json = await response.Content.ReadAsStringAsync();
                        var data = JsonSerializer.Deserialize<MessagesModel>(json);
                        if (data != null && data.Data.Any())
                        {
                            _listId.AddRange(data.Data);
                            url = data.Paging.Next;
                            LoadingText = _listId.Count + " ID";
                        }
                        if (string.IsNullOrEmpty(url))
                        {
                            LoadingText = "Kiểm tra danh sách tin nhăn xong";
                            break;
                        }

                        await Task.Delay(Random.Shared.Next(1000, 2000));
                    }

                    if (_listId.Any())
                    {
                        string[] lsMesssage = Messages.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                        int maxLen = lsMesssage.Length;
                        int messageLen = 0;

                        foreach (var dataMessages in _listId)
                        {

                            var client = new HttpClient();
                            var request = new HttpRequestMessage(HttpMethod.Post, "https://graph.facebook.com/v13.0/" + dataMessages.Id);
                            request.Headers.Add("Cookie", "fr=0LY9iVKHXBFJV4vVE..BlN-wj.Ng.AAA.0.0.BlN_hU.AWUIewMuF4E");
                            var collection = new List<KeyValuePair<string, string>>();
                            collection.Add(new("access_token", AccessToken));
                            collection.Add(new("body", lsMesssage[messageLen]));
                            var content = new FormUrlEncodedContent(collection);
                            request.Content = content;
                            var response = await client.SendAsync(request);
                            response.EnsureSuccessStatusCode();

                            var json = await response.Content.ReadAsStringAsync();
                            LoadingText = "Id=" + dataMessages.Id + "\n" + json;
                            Friends = $"[{DateTime.Now}] "+"Id=" + dataMessages.Id + "-" + json + "\n" + Friends;
                            messageLen++;
                            if (messageLen == maxLen)
                            {
                                messageLen = 0;
                            }
                            await Task.Delay(Random.Shared.Next(6000,10000));
                        }
                        MessageBox.Show("Gửi tin nhắn xong!", "Thông báo", MessageBoxButton.OK);
                    }
                    else
                    {
                        MessageBox.Show("Bạn không có tin nhắn nào!", "Thông báo", MessageBoxButton.OK);
                    }
                }
            }
            catch (Exception e)
            {
                Crashes.TrackError(e);
                MessageBox.Show("Lỗi: "+e.Message+"\nVui lòng liên hệ với đội phát triển để thông bao lỗi", "Thông báo", MessageBoxButton.OK);
            }

            IsBusy = false;
        }
    }
}
