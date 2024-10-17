using Microsoft.Extensions.DependencyInjection;
using PRN221_Admin.ViewModels;
using PRN221_BusinessLogic.Interface;
using PRN221_BusinessLogic.Service;
using PRN221_DataAccess.DAOs;
using PRN221_Repository.AccountRepo;
using System.Configuration;
using System.Data;
using System.Windows;

namespace PRN221_Admin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static IServiceProvider ServiceProvider { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            var mainwindow = ServiceProvider.GetRequiredService<DashboardMU>();
            //var studentViewModel = ServiceProvider.GetRequiredService<MemberViewModel>();
            //mainwindow.DataContext = studentViewModel;
            mainwindow.Show();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            // Đăng ký các service, repository, DAO mà bạn đã có sẵn services.
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<AccountDAO>();
            services.AddTransient<FarmerModelView>();

            // Đăng ký MainWindow
            services.AddSingleton<DashboardMU>();
            //services.AddSingleton<MemberViewModel>();
        }
    }

}
