using Microsoft.Win32;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ImTools;
using MetaTools.Models;
using Microsoft.AppCenter.Analytics;
using System.Security.Cryptography;
using MetaTools.Repositories;

namespace MetaTools.ViewModels
{
    public class AccountsViewModel : RegionViewModelBase
    {
        private string _accounts;
        private string _pathOpenFileName;
        private bool _isAccount;
        private bool _isCookie = true;
        private ObservableCollection<AccountInfo> _accountInfos = new ObservableCollection<AccountInfo>();
        private readonly IAccountInfoRepository _accountInfoRepository;
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

        public bool IsCookie
        {
            get => _isCookie;
            set => SetProperty(ref _isCookie, value);
        }

        public bool IsAccount
        {
            get => _isAccount;
            set => SetProperty(ref _isAccount, value);
        }

        public ICommand AddAccountsCommand { get; private set; }

        public ObservableCollection<AccountInfo> AccountInfos
        {
            get => _accountInfos;
            set => SetProperty(ref _accountInfos, value);
        }

        public AccountsViewModel(IRegionManager regionManager, IAccountInfoRepository accountInfoRepository) : base(regionManager)
        {
            _accountInfoRepository = accountInfoRepository;
            Analytics.TrackEvent("AccountsViewModel");
            ResetInputCommand = new DelegateCommand(ResetInput);
            OpenfileCommand = new DelegateCommand(Openfile);
            AddAccountsCommand = new DelegateCommand(AddAccounts);
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            var data = await _accountInfoRepository.GetAllAccountsAsync();
            if (data != null && data.Any())
            {
                AccountInfos = new ObservableCollection<AccountInfo>(data);
            }
        }

        private async void AddAccounts()
        {
            Analytics.TrackEvent("AddAccounts");
            if (string.IsNullOrEmpty(Accounts)/* || string.IsNullOrEmpty(PathOpenFileName)*/)
            {
                MessageBox.Show("Input empty", "Notification", MessageBoxButton.OK);
                return;
            }

            var lsAccounts = Accounts.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            lsAccounts.ForEach(account =>
            {
                if (account.Contains("c_user="))
                {
                    var uid = account.Split("c_user=")[1].Trim().Split(";")[0].Trim();
                    var acc = new AccountInfo()
                    {
                        Uid = uid,
                        Cookie = account,
                        DateCreate = DateTime.Now.ToString("G"),
                        DateChange = DateTime.Now.ToString("G"),
                    };
                    AccountInfos.Add(acc);
                }
                else
                {
                    // 100055508814356|XP78ghymfw81951|N4A2PDIGF5UYBQ6L5PEBOI2KSJ5YCTBW|rebeccacutsingerv@hotmail.com|Mailmmo920
                    var ls = account.Split("|");
                    var acc = new AccountInfo()
                    {
                        Uid = ls[0],
                        Password = ls[1],
                        TwoFaCode = ls[2],
                        Email = ls[3],
                        EmailPassword = ls[4],
                        DateCreate = DateTime.Now.ToString("G"),
                        DateChange = DateTime.Now.ToString("G"),
                    };
                    AccountInfos.Add(acc);
                }
            });
            var add = await _accountInfoRepository.AddAccountsAsync(AccountInfos);
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
