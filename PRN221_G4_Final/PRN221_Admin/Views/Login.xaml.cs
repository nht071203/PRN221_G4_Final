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
using System.Windows.Shapes;

namespace PRN221_Admin.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        public PlotModel PlotModel { get; set; }

        private ExpertViewModel _ExpertViewModel;
        public NewsViewModel _NewsViewModel;
        private FarmerViewModel _farmerViewModel;
        public FarmerModelView FarmerView { get; set; }
        public Login(LoginViewModel viewModel, FarmerModelView FarmerView, ExpertViewModel expertViewModel, NewsViewModel newsViewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            _ExpertViewModel = expertViewModel;
            _NewsViewModel = newsViewModel;
            this.FarmerView = FarmerView;
            DataContext = this;
        }
        public Login()
        {
            InitializeComponent();
        }
    }
}
