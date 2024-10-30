using System.Windows;
using OxyPlot;
using PRN221_Admin.Views;
using PRN221_Admin.ViewModels;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.IO;

namespace PRN221_Admin
{
    /// <summary>
    /// Interaction logic for DashboardMU.xaml
    /// </summary>
    public partial class DashboardMU : Window
    {


        private ExpertViewModel _ExpertViewModel;
        public NewsViewModel _NewsViewModel;
       // private FarmerModelView _farmerViewModel;
        private LoginViewModel _loginViewModel;
        public FarmerModelView FarmerView { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public SeriesCollection LastHourSeries { get; set; }
        public SeriesCollection LastHourSeries1 { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public PlotModel PlotModel { get; set; }
        public ProfileModelView ProfileView { get; set; }


        public DashboardMU(ExpertViewModel expertViewModel, NewsViewModel newsViewModel, FarmerModelView farmerViewModel, LoginViewModel login, ProfileModelView profile)
        {
            InitializeComponent();
            FarmerView = farmerViewModel;
            _ExpertViewModel = expertViewModel;
            _NewsViewModel = newsViewModel;
            _loginViewModel = login;
            ProfileView = profile;
            DataContext = this;
            DashboardPage.Content = new LoginPage(_loginViewModel, expertViewModel, farmerViewModel, newsViewModel, profile);
        }

        public DashboardMU()
        {
         InitializeComponent();
            //DashboardPage.Content = new LoginPage(_loginViewModel,_ExpertViewModel, _);
        }

        
       public DashboardMU(FarmerModelView farmerViewModel)
        {
            InitializeComponent();
            FarmerView = farmerViewModel;
           // DashboardPage.Content = new LoginPage(_loginViewModel, _ExpertViewModel);
        }


        
        /*public DashboardMU(ProfileModelView profileViewModel, FarmerModelView farmerViewModel)
        {
            InitializeComponent();
            FarmerView = farmerViewModel;
            ProfileView = profileViewModel;
            NoiDung.Content = new ProfileAdmin(ProfileView);
        }


        private void Button_Dashboard(object sender, RoutedEventArgs e)
        {
            NoiDung.Content = new ProfileAdmin(ProfileView);
        }*/
    }
}
