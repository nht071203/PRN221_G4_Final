using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
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
using PRN221_DataAccess.DAOs;
using PRN221_DataAccess;
using PRN221_Admin.ViewModels;
using System.Windows.Threading;
using System.Windows.Media.Media3D;

namespace PRN221_Admin
{
    /// <summary>
    /// Interaction logic for ThongKePost.xaml
    /// </summary>
    public partial class ThongKePost : Page
    {

        public ThongKePost()
        {
            InitializeComponent();
            DataContext = new ThongKeModelView();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Content = DateTime.Now.ToString("HH:mm:ss");
        }

        private void ThemeToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            // Chuyển sang chế độ tối
            MainGrid.Background = new SolidColorBrush(Colors.Black);
            ThemeToggleButton.Foreground = new SolidColorBrush(Colors.White);
        }

        private void ThemeToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            // Chuyển sang chế độ sáng
            MainGrid.Background = new SolidColorBrush(Colors.White);
            ThemeToggleButton.Foreground = new SolidColorBrush(Colors.Black);
        }

   

    }
}
