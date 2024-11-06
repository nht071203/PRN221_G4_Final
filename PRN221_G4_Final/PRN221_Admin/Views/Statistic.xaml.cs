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
using System.Windows.Threading;

namespace PRN221_Admin.Views
{
    /// <summary>
    /// Interaction logic for Statistic.xaml
    /// </summary>
    public partial class Statistic : Page
    {

        private readonly StatisticModelView _StatisticModelView;
        public Statistic()
        {
            InitializeComponent();
            _StatisticModelView = (StatisticModelView)App.ServiceProvider.GetService(typeof(StatisticModelView));
            DataContext = _StatisticModelView;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
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
