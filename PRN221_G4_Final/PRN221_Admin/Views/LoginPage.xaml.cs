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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private ExpertViewModel expertViewModel;
        private FarmerModelView farmerViewModel;
        private NewsViewModel NewsViewModel;
        private ProfileModelView profileViewModel;
        public LoginPage(LoginViewModel login, 
            ExpertViewModel expert, FarmerModelView farmerViewModel, NewsViewModel NewsViewModel, ProfileModelView profile)
        {
            InitializeComponent();
            DataContext = login;
            expertViewModel = expert;
            this.farmerViewModel = farmerViewModel;
            this.NewsViewModel = NewsViewModel;
            profileViewModel = profile;
        }
    }
}
