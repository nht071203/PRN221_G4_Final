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
using System.Windows.Shapes;
using OxyPlot;
using OxyPlot.Series;
using System.Windows;
using OxyPlot.Axes;
using PRN221_Admin.Views;
using PRN221_Admin.ViewModels;

namespace PRN221_Admin
{
    /// <summary>
    /// Interaction logic for DashboardMU.xaml
    /// </summary>
    public partial class DashboardMU : Window
    {
        public PlotModel PlotModel { get; set; }

        private ExpertViewModel _ExpertViewModel;
        private NewsViewModel _NewsViewModel;
        private FarmerViewModel _farmerViewModel;
        public FarmerModelView FarmerView { get; set; }
        
        public DashboardMU(ExpertViewModel expertViewModel, NewsViewModel newsViewModel, FarmerModelView farmerViewModel)
        {
            InitializeComponent();
            FarmerView = farmerViewModel;
            _ExpertViewModel = expertViewModel;
            _NewsViewModel = newsViewModel;
          
        }

        public DashboardMU()
        {
         InitializeComponent();
        }

        
       public DashboardMU(FarmerModelView farmerViewModel)
        {
            InitializeComponent();
            FarmerView = farmerViewModel;
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
