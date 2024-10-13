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

namespace PRN221_Admin
{
    /// <summary>
    /// Interaction logic for DashboardMU.xaml
    /// </summary>
    public partial class DashboardMU : Window
    {
        public PlotModel PlotModel { get; set; }
        public DashboardMU()
        {
            InitializeComponent();
            PlotModel = new PlotModel { Title = "Post Chart" };

            PlotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Month"
            });
            PlotModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Posts"
            });



            var lineSeries = new LineSeries
            {
                Title = "Bai post trong thang",
                MarkerType = MarkerType.Diamond,
                Color = OxyColor.FromArgb(100, 255, 0, 0)    //đỏ ,xanh, xanh da trời
            };


    
            lineSeries.Points.Add(new DataPoint(1, 18));
            lineSeries.Points.Add(new DataPoint(2, 12));
            lineSeries.Points.Add(new DataPoint(3, 8));
            lineSeries.Points.Add(new DataPoint(4, 15));
            lineSeries.Points.Add(new DataPoint(5, 0));
            lineSeries.Points.Add(new DataPoint(6, 18));
            lineSeries.Points.Add(new DataPoint(7, 12));
            lineSeries.Points.Add(new DataPoint(8, 8));
            lineSeries.Points.Add(new DataPoint(9, 15));
            lineSeries.Points.Add(new DataPoint(10, 12));
            lineSeries.Points.Add(new DataPoint(11, 8));
            lineSeries.Points.Add(new DataPoint(12, 15));


            PlotModel.Series.Add(lineSeries);
              DataContext = this;
       
        }

    
    }
}
