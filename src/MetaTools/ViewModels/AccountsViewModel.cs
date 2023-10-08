using Microsoft.Win32;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Windows;
using System.Windows.Input;

namespace MetaTools.ViewModels
{
    public class AccountsViewModel : RegionViewModelBase
    {
        private string _accounts;
        private string _pathOpenFileName;
        private bool _isAccount;
        private bool _isCookie = true;
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
        public AccountsViewModel(IRegionManager regionManager) : base(regionManager)
        {
            ResetInputCommand = new DelegateCommand(ResetInput);
            OpenfileCommand = new DelegateCommand(Openfile);
            AddAccountsCommand = new DelegateCommand(AddAccounts);
        }

        private void AddAccounts()
        {
            if (string.IsNullOrEmpty(Accounts) || string.IsNullOrEmpty(PathOpenFileName))
            {
                MessageBox.Show("Input empty", "Notification", MessageBoxButton.OK);
                return;
            }
        }

        private void Openfile()
        {
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
            if (string.IsNullOrEmpty(Accounts))
                Accounts = string.Empty;
            if (string.IsNullOrEmpty(PathOpenFileName))
                PathOpenFileName = string.Empty;
        }
    }
}
