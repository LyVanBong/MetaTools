using MetaTools.Services.Facebook;
using MetaTools.Services.TwoFactorAuthentication;

namespace MetaTools
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override  void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Config AppCenter
            AppCenter.Start("1eaba9dd-3cb4-40c0-8757-124b3b247488",
                typeof(Analytics), typeof(Crashes));

             AppCenter.SetEnabledAsync(true);
             Crashes.SetEnabledAsync(true);
             Analytics.SetEnabledAsync(true);

             // Định danh app
           var installId = AppCenter.GetInstallIdAsync();

            // Tự đặt id
            //AppCenter.SetUserId("your-user-id");

            // Khởi tạo background service
            var task = Container.Resolve<IBackgroundTaskService>();
            task?.StartAsync(new CancellationToken());
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CommentView, CommentViewModel>();
            containerRegistry.RegisterForNavigation<DashboardView, DashboardViewModel>();
            containerRegistry.RegisterForNavigation<AccountsView, AccountsViewModel>();

            containerRegistry.RegisterDialog<NotificationDialog, NotificationDialogViewModel>();

            containerRegistry.RegisterSingleton<IUserAgentService, UserAgentService>();
            containerRegistry.RegisterSingleton<IAccountInfoRepository, AccountInfoRepository>();
            containerRegistry.RegisterSingleton<IRequestProvider, RequestProvider.RequestProvider>();
            containerRegistry.RegisterSingleton<IFacebookService, FacebookService>();
            containerRegistry.RegisterSingleton<ITwoFactorAuthentication, TwoFactorAuthentication>();
            containerRegistry.RegisterSingleton<IAccountService, AccountService>();
            containerRegistry.RegisterSingleton<IBackgroundTaskService, BackgroundTaskService>();
        }
    }
}