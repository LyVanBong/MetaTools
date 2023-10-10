using MetaTools.Models;
using MetaTools.Repositories;
using MetaTools.Services.UserAgent;
using Microsoft.AppCenter.Analytics;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MetaTools.ViewModels
{
    public class AccountsViewModel : RegionViewModelBase
    {
        private string _accounts;
        private string _pathOpenFileName;
        private ObservableCollection<AccountInfo> _accountInfos = new ObservableCollection<AccountInfo>();
        private readonly IAccountInfoRepository _accountInfoRepository;
        private readonly IUserAgentService _userAgentService;
        private bool _isBusy;
        private Visibility _visibility1 = Visibility.Hidden;
        private Visibility _visibility = Visibility.Visible;
        public ICommand ResetInputCommand { get; private set; }

        public string Accounts
        {
            get => _accounts;
            set => SetProperty(ref _accounts, value);
        }

        public ICommand OpenfileCommand { get; private set; }

        public string PathOpenFileName
        {
            get => _pathOpenFileName;
            set => SetProperty(ref _pathOpenFileName, value);
        }
        public ICommand AddAccountsCommand { get; private set; }

        public ObservableCollection<AccountInfo> AccountInfos
        {
            get => _accountInfos;
            set => SetProperty(ref _accountInfos, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value, () =>
            {
                if (value)
                {
                    Visibility1 = Visibility.Visible;
                    Visibility = Visibility.Hidden;
                }
                else
                {
                    Visibility1 = Visibility.Hidden;
                    Visibility = Visibility.Visible;
                }
            });
        }

        public Visibility Visibility
        {
            get => _visibility;
            set => SetProperty(ref _visibility, value);
        }

        public Visibility Visibility1
        {
            get => _visibility1;
            set => SetProperty(ref _visibility1, value);
        }

        public AccountsViewModel(IRegionManager regionManager, IAccountInfoRepository accountInfoRepository, IUserAgentService userAgentService) : base(regionManager)
        {
            _accountInfoRepository = accountInfoRepository;
            _userAgentService = userAgentService;
            Analytics.TrackEvent("AccountsViewModel");
            ResetInputCommand = new DelegateCommand(ResetInput);
            OpenfileCommand = new DelegateCommand(Openfile);
            AddAccountsCommand = new DelegateCommand(AddAccounts);
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            await GetAccounts();
        }
        /// <summary>
        /// Lấy danh sách tài khoản
        /// </summary>
        /// <returns></returns>
        private async Task GetAccounts()
        {
            var data = await _accountInfoRepository.GetAllAccountsAsync();
            if (data != null && data.Any())
            {
                AccountInfos = new ObservableCollection<AccountInfo>(data);
            }
        }

        private async void AddAccounts()
        {
            Analytics.TrackEvent("AddAccounts");
            if (string.IsNullOrEmpty(Accounts))
            {
                MessageBox.Show("Input empty", "Notification", MessageBoxButton.OK);
                return;
            }
            if (IsBusy) return;
            IsBusy = true;

            var lsAccounts = Accounts.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            foreach (var account in lsAccounts)
            {
                var ua = await _userAgentService.Generate();
                AccountInfo acc;
                if (account.Contains("c_user="))
                {
                    var uid = account.Split("c_user=")[1].Trim().Split(";")[0].Trim();
                    acc = new AccountInfo()
                    {
                        Uid = uid,
                        Cookie = account,
                        DateCreate = DateTime.Now.ToString("G"),
                        DateChange = DateTime.Now.ToString("G"),
                        Useragent = ua.Ua,
                    };
                }
                else
                {
                    // 100055508814356|XP78ghymfw81951|N4A2PDIGF5UYBQ6L5PEBOI2KSJ5YCTBW|rebeccacutsingerv@hotmail.com|Mailmmo920

                    string[] ls = account.Split("|");
                    acc = new AccountInfo()
                    {
                        Uid = ls[0],
                        Password = ls[1],
                        TwoFaCode = ls[2],
                        Email = ls[3],
                        EmailPassword = ls[4],
                        Useragent = ua.Ua,
                        DateCreate = DateTime.Now.ToString("G"),
                        DateChange = DateTime.Now.ToString("G"),
                    };
                }
                await _accountInfoRepository.AddAccountAsync(acc);
            }

            await GetAccounts();
            ResetInput();
            MessageBox.Show("Add account done", "Notification", MessageBoxButton.OK);
            IsBusy = false;
        }

        private void Openfile()
        {
            Analytics.TrackEvent("Openfile");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open File Accounts";
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (openFileDialog.ShowDialog() == true)
            {
                PathOpenFileName = openFileDialog.FileName;
                var readFile = File.ReadAllText(PathOpenFileName);
                Accounts = readFile.Trim() + "\n" + Accounts;
            }
        }

        private void ResetInput()
        {
            Analytics.TrackEvent("ResetInput");
            Accounts = string.Empty;
            PathOpenFileName = string.Empty;
        }
    }
}
