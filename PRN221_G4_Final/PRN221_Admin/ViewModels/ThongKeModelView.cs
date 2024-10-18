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
            TotalExpertCount = await AccountDAO.Instance.GetTotaExpertCountAsync();
            TotalServiceCount = await ServiceDAO.Instance.GetTotalServicesCount();

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

        private int _totalNewsCount;
        public int TotalNewsCount
        {
            get { return _totalNewsCount; }
            set
            {
                if (_totalNewsCount != value)
                {
                    _totalNewsCount = value;
                    OnPropertyChanged(nameof(TotalNewsCount));
                }
            }
        }

        private int _totalFarmerCount;
        public int TotalFarmerCount
        {
            get { return _totalFarmerCount; }
            set
            {
                if (_totalFarmerCount != value)
                {
                    _totalFarmerCount = value;
                    OnPropertyChanged(nameof(TotalFarmerCount));
                }
            }
        }

        private int _totalExpertCount;
        public int TotalExpertCount
        {
            get { return _totalExpertCount; }
            set
            {
                if (_totalExpertCount != value)
                {
                    _totalExpertCount = value;
                    OnPropertyChanged(nameof(TotalExpertCount));
                }
            }
        }

        private int _totalServiceCount;
        public int TotalServiceCount
        {
            get { return _totalServiceCount; }
            set
            {
                if (_totalServiceCount != value)
                {
                    _totalServiceCount = value;
                    OnPropertyChanged(nameof(TotalServiceCount));
                }
            }
        }



    }
}