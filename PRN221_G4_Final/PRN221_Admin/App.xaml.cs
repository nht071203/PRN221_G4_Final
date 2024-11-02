using Microsoft.Extensions.DependencyInjection;
using PRN221_Admin.ViewModels;
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
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IPostImageRepository, PostImageRepository>(); // Đăng ký IPostImageRepository và PostImageRepository
            services.AddTransient<IPostImageService, PostImageService>();
            services.AddTransient<PostImageDAO>();

            services.AddTransient<ILikePostRepository, LikePostRepository>(); // Đăng ký IPostImageRepository và PostImageRepository
            services.AddTransient<LikePostDAO>();

            services.AddTransient<ICommentRepository, CommentRepository>(); // Đăng ký IPostImageRepository và PostImageRepository
            services.AddTransient<CommentPostDAO>();

            services.AddTransient<ISharePostRepository, SharePostRepository>(); // Đăng ký IPostImageRepository và PostImageRepository
            services.AddTransient<SharePostDAO>();


            services.AddTransient<IPostRepository, PostRepository>(); // Đăng ký IPostRepository và PostRepository
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<PostDAO>();

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
        }

    }
}
