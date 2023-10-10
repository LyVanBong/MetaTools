using DryIoc;
using MetaTools.Repositories;
using MetaTools.Services.UserAgent;
using MetaTools.ViewModels;
using MetaTools.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prism.Ioc;
using System.Windows;
using MetaTools.BackgroundTasks;
using MetaTools.RequestProvider;

namespace MetaTools
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            var builder = WebApplication.CreateBuilder();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHostedService<TestTask>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction() || app.Environment.IsStaging())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.RunAsync();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AppCenter.Start("1eaba9dd-3cb4-40c0-8757-124b3b247488",
                typeof(Analytics), typeof(Crashes));
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
        }
    }
}