    //using System;
    //using System.Collections.Generic;
    //using System.Linq;
    //using System.Text;
    //using System.Threading.Tasks;
    //using System.Windows;
    //using System.Windows.Controls;
    //using System.Windows.Data;
    //using System.Windows.Documents;
    //using System.Windows.Input;
    //using System.Windows.Media;
    //using System.Windows.Media.Imaging;
    //using System.Windows.Shapes;
    //using OxyPlot;
    //using OxyPlot.Series;
    //using System.Windows;
    //using OxyPlot.Axes;
    //using PRN221_Admin.Views;
    //using PRN221_Admin.ViewModels;
    //using LiveCharts.Defaults;
    //using LiveCharts.Wpf;
    //using LiveCharts;
    //using System.IO;

    //namespace PRN221_Admin
    //{
    //    /// <summary>
    //    /// Interaction logic for DashboardMU.xaml
    //    /// </summary>
    //    public partial class DashboardMU : Window
    //    {
    //        public PlotModel PlotModel { get; set; }
    //        public FarmerModelView FarmerView { get; set; }
    //        public DashboardMU()
    //        {
    //            InitializeComponent();
    //        }
    //       public DashboardMU(FarmerModelView farmerViewModel)
    //        {
    //            InitializeComponent();
    //            FarmerView = farmerViewModel;
    //        }


    //        private void Button_ThongKe(object sender, RoutedEventArgs e)
    //        {
    //            NoiDung.Content = new ThongKePost();

    //        }

    //        private void Button_ManagerNew(object sender, RoutedEventArgs e)
    //        {
    //            NoiDung.Content = new NewsPage();
    //        }

    //        private void Button_ManageExpert(object sender, RoutedEventArgs e)
    //        {
    //            NoiDung.Content = new ExpertPage();
    //        }


    //        private void Button_ManageFarmer(object sender, RoutedEventArgs e)
    //        {
    //            NoiDung.Content = new FarmerPage(FarmerView);
    //        }
    //    }
    //}



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

            public SeriesCollection SeriesCollection { get; set; }
            public SeriesCollection LastHourSeries { get; set; }
            public SeriesCollection LastHourSeries1 { get; set; }
            public string[] Labels { get; set; }
            public Func<double, string> Formatter { get; set; }

            public PlotModel PlotModel { get; set; }
            public FarmerModelView FarmerView { get; set; }

        public DashboardMU(FarmerModelView farmerViewModel)
            {
                InitializeComponent();
                FarmerView = farmerViewModel;
            }

            public DashboardMU()
            {
                InitializeComponent();
                SeriesCollection = new SeriesCollection();
            }

        private void Button_ThongKe(object sender, RoutedEventArgs e)
        {
            NoiDung.Content = new ThongKePost();
        }

        private void Button_Farmer(object sender, RoutedEventArgs e)
        {
            NoiDung.Content = new FarmerPage(FarmerView);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NoiDung.Content = new BieuDo();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
    }
