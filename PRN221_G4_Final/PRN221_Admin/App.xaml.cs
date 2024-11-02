using Microsoft.Extensions.DependencyInjection;
using PRN221_Admin.ViewModels;
using PRN221_Admin.Views;
using PRN221_BusinessLogic.Interface;
using PRN221_BusinessLogic.Service;
using PRN221_DataAccess.DAOs;
using PRN221_Repository.AccountRepo;
using PRN221_Repository.CommentRepo;
using PRN221_Repository.LikePostRepo;
using PRN221_Repository.NewsRepo;
using PRN221_Repository.PostImageRepo;
using PRN221_Repository.PostsRepo;
using PRN221_Repository.SharePostRepo;
using System.Windows;

namespace PRN221_Admin
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();


            var mainWindow = ServiceProvider.GetRequiredService<DashboardMU>();
            mainWindow.DataContext = ServiceProvider.GetRequiredService<ThongKeModelView>();
            mainWindow.Show();

            var mainwindow = ServiceProvider.GetRequiredService<DashboardMU>();
            var dasboardPage = ServiceProvider.GetRequiredService<DashBoardPage>();

            var studentViewModel = ServiceProvider.GetRequiredService<ExpertViewModel>();
            var newsViewModel = ServiceProvider.GetRequiredService<NewsViewModel>();
            //var farmerViewModel = ServiceProvider.GetRequiredService<FarmerViewModel>();
            var farmerMU = ServiceProvider.GetRequiredService<FarmerModelView>();

            var loginView = ServiceProvider.GetRequiredService<LoginViewModel>();

            var profileMU = ServiceProvider.GetRequiredService<ProfileModelView>();


            mainwindow.DataContext = studentViewModel;
            mainwindow.DataContext = newsViewModel;
            //mainwindow.DataContext = farmerViewModel;
            mainwindow.DataContext = farmerMU;

            dasboardPage.DataContext = studentViewModel;
            dasboardPage.DataContext = newsViewModel;
            dasboardPage.DataContext = farmerMU;
            mainwindow.Show();
           



        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IPostImageRepository, PostImageRepository>(); 
            services.AddTransient<IPostImageService, PostImageService>();
            services.AddTransient<PostImageDAO>();

            services.AddTransient<ILikePostRepository, LikePostRepository>(); 
            services.AddTransient<LikePostDAO>();

            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<CommentPostDAO>();

            services.AddTransient<ISharePostRepository, SharePostRepository>(); 
            services.AddTransient<SharePostDAO>();


            services.AddTransient<IPostRepository, PostRepository>(); 
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<PostDAO>();



            // Đăng ký các service, repository, DAO mà bạn đã có sẵn services.

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<AccountDAO>();

            services.AddTransient<ICategoryNewsRepository, CategoryNewsRepository>();
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<INewsRepository, NewsRepository>();
            services.AddTransient<NewsDAO>();


            // Đăng ký CategoryNewsDAO
            services.AddTransient<CategoryNewsDAO>(); // Đăng ký CategoryNewsDAO

            services.AddSingleton<IPostService, PostService>();

            // Đăng ký các ViewModels
            services.AddTransient<ThongKeModelView>();
            services.AddTransient<ProfileModelView>();
            services.AddTransient<FarmerModelView>();
            services.AddTransient<ExpertViewModel>();
            services.AddTransient<NewsViewModel>();
            services.AddTransient<DashboardMU>();


      
      
            services.AddTransient<CategoryNewsDAO>();
       

            services.AddTransient<LoginViewModel>();


            services.AddTransient<ProfileModelView>();


            /*// Đăng ký ViewModel
            services.AddSingleton<ExpertViewModel>();
            services.AddSingleton<NewsViewModel>();
            services.AddSingleton<FarmerViewModel>();*/

            // Đăng ký MainWindow
           services.AddSingleton<DashboardMU>();
            services.AddSingleton<DashBoardPage>();
            
            // services.AddSingleton<AccountService>();

        }

    }
}
