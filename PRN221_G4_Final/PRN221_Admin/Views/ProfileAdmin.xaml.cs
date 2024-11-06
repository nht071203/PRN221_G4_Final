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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PRN221_Admin.Views
{
    /// <summary>
    /// Interaction logic for ProfileAdmin.xaml
    /// </summary>
    public partial class ProfileAdmin : Page
    {
        public ProfileAdmin(ProfileModelView viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            Loaded += ProfileAdmin_Loaded;
        }
        private void ProfileAdmin_Loaded(object sender, RoutedEventArgs e)
        {
            // Lấy Storyboard từ Resources
            Storyboard sb = (Storyboard)this.Resources["MarqueeStoryboard"];
            sb.Begin();
        }

    }
}
