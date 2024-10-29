﻿using System.Windows;
using OxyPlot;
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
        public NewsViewModel _NewsViewModel;
        private FarmerViewModel _farmerViewModel;
        private LoginViewModel _loginViewModel;
        public FarmerModelView FarmerView { get; set; }

        public DashboardMU(ExpertViewModel expertViewModel, NewsViewModel newsViewModel, FarmerModelView farmerViewModel, LoginViewModel login)
        {
            InitializeComponent();
            FarmerView = farmerViewModel;
            _ExpertViewModel = expertViewModel;
            _NewsViewModel = newsViewModel;
            _loginViewModel = login;
            DataContext = this;
            DashboardPage.Content = new LoginPage(_loginViewModel, expertViewModel, farmerViewModel, newsViewModel);
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


        /*private void Button_ThongKe(object sender, RoutedEventArgs e)
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
        }*/

    /*    private void Button_Click(object sender, RoutedEventArgs e)
        {
            DashboardPage.Content = new LoginPage(_loginViewModel);
        }*/
    }
}
