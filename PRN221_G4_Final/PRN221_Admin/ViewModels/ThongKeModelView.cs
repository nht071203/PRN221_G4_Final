using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using PRN221_DataAccess.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Admin.ViewModels
{
   public class ThongKeModelView : BaseViewModel
    {
        public PlotModel PlotModel { get; set; }
        public ThongKeModelView()
        {
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

            LoadData(); 
            //DataContext = this;
        }



        private async void LoadData()
        {
            var newsCountByMonth = await NewsDAO.Instance.GetNewsCountByMonth(); 

            var lineSeries = new LineSeries
            {
                Title = "Số bài post trong tháng",
                MarkerType = MarkerType.Diamond,
                Color = OxyColor.FromArgb(255, 255, 0, 0)
            };

            foreach (var item in newsCountByMonth)
            {
                var month = DateTime.ParseExact(item.Month, "yyyy-MM", null).Month; 
                lineSeries.Points.Add(new DataPoint(month, item.Count)); 
            }

            PlotModel.Series.Add(lineSeries);
            PlotModel.InvalidatePlot(true); 
        }
    }
}
