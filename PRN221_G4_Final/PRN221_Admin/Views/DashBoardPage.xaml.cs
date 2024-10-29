using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using OxyPlot;
using PRN221_Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PRN221_Admin.Views
{
    /// <summary>
    /// Interaction logic for DashBoardPage.xaml
    /// </summary>
    public partial class DashBoardPage : Page
    {
        public PlotModel PlotModel { get; set; }

        private ExpertViewModel _ExpertViewModel;
        public NewsViewModel _NewsViewModel;
        private FarmerViewModel _farmerViewModel;
        private LoginViewModel _loginViewModel;
        public FarmerModelView FarmerView { get; set; }
        public DashBoardPage(ExpertViewModel expertViewModel, NewsViewModel newsViewModel, FarmerModelView farmerViewModel)
        {
            InitializeComponent();
            FarmerView = farmerViewModel;
            _ExpertViewModel = expertViewModel;
            _NewsViewModel = newsViewModel;
            DataContext = this;
        }

        public DashBoardPage()
        {
            InitializeComponent();
        }

        private void Button_ThongKe(object sender, RoutedEventArgs e)
        {
            NoiDung.Content = new ThongKePost();

        }

        private void Button_ManagerNew(object sender, RoutedEventArgs e)
        {
            NoiDung.Content = new NewsPage(_NewsViewModel);
        }

        private void Button_ManageExpert(object sender, RoutedEventArgs e)
        {
            NoiDung.Content = new ExpertPage(_ExpertViewModel);
        }


        private void Button_ManageFarmer(object sender, RoutedEventArgs e)
        {
            NoiDung.Content = new FarmerPage(FarmerView);
        }
    }
}
