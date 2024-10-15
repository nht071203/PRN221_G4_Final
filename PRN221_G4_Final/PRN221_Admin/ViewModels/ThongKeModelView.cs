using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using PRN221_DataAccess.DAOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Admin.ViewModels
{
   public class ThongKeModelView : BaseViewModel
    {
        public NewsDAO newsDAO;

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
        }



        private async void LoadData()
        {
            var newsCountByMonth = await NewsDAO.Instance.GetNewsCountByMonth();
            TotalNewsCount = await NewsDAO.Instance.GetTotalNewsCountAsync();
            TotalFarmerCount = await AccountDAO.Instance.GetTotalFarmerCountAsync();


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

        private int _totalNewsCount; // Trường riêng để lưu tổng số lượng bài viết

        public int TotalNewsCount
        {
            get { return _totalNewsCount; }
            set
            {
                if (_totalNewsCount != value)
                {
                    _totalNewsCount = value;
                    OnPropertyChanged(nameof(TotalNewsCount)); // Thông báo cho UI rằng giá trị đã thay đổi
                }
            }
        }


        private int _totalFarmerCount; // Trường riêng để lưu tổng số lượng bài viết

        public int TotalFarmerCount
        {
            get { return _totalFarmerCount; }
            set
            {
                if (_totalFarmerCount != value)
                {
                    _totalFarmerCount = value;
                    OnPropertyChanged(nameof(TotalFarmerCount)); // Thông báo cho UI rằng giá trị đã thay đổi
                }
            }
        }


    }
}
