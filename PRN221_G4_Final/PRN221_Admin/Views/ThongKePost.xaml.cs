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
        }

    
   
    }
}
