using OxyPlot.Axes;
using OxyPlot;
using OxyPlot.Series;
using PRN221_BusinessLogic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using PRN221_Models.Models;


namespace PRN221_Admin.ViewModels
{

   public class StatisticModelView : BaseViewModel
    {
        public PlotModel PlotModel { get; set; }
        private readonly INewsService newsService;
        private readonly IAccountService accountService;
        private readonly IPostService postService;

        public StatisticModelView(INewsService newsService, IAccountService accountService, IPostService postService)
        {
            this.newsService = newsService;
            this.accountService = accountService;
            this.postService = postService;

            PlotModel = new PlotModel { Title = "Post Chart" };
            PlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Day" });
            PlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Posts" });
      
            LoadDataYearMonth = new RelayCommand(async (obj) => await LoadDataPoDay());
            LoadData();
 
        }


        public ICommand LoadDataYearMonth { get; set; }



        private int _year; // Năm nhập vào từ người dùng
        public int Year
        {
            get { return _year; }
            set
            {
                if (_year != value)
                {
                    _year = value;
                    OnPropertyChanged(nameof(Year)); // Thông báo thay đổi thuộc tính
                }
            }
        }


        private int _month; // Năm nhập vào từ người dùng
        public int Month
        {
            get { return _month; }
            set
            {
                if (_month != value)
                {
                    _month = value;
                    OnPropertyChanged(nameof(Month)); // Thông báo thay đổi thuộc tính
                }
            }
        }

        private Account _topFarmer;
        public Account TopFarmer
        {
            get => _topFarmer;
            set
            {
                _topFarmer = value;
                OnPropertyChanged(nameof(TopFarmer));
            }
        }
        private Account _topExpert;
        public Account TopExpert
        {
            get => _topExpert;
            set
            {
                _topExpert = value;
                OnPropertyChanged(nameof(TopExpert));
            }
        }
        private async Task LoadData()
        {
     

            TopFarmer = await postService.FarmerWithMostPosts() ?? new Account();
            TopExpert = await postService.ExpertWithMostPosts() ?? new Account();

        }


        private async Task LoadDataPoDay()
        {
            int currentYear = Year > 0 ? Year : DateTime.Now.Year;
            int selectedYear = Year > 0 ? Year : DateTime.Now.Year;
            int selectedMonth = Month > 0 ? Month : DateTime.Now.Month;

            var newsCountByDay = await newsService.GetNewsCountByDay(selectedYear, selectedMonth);

            if (Year < 2020 || Year > currentYear && Month < 1 || Month > 12)
            {
                // Thông báo cho người dùng nếu năm không hợp lệ
                MessageBox.Show("Số năm không hợp lệ");
                return;
            }


            var lineSeries = new LineSeries
            {
                Title = "Số bài post trong ngày",
                MarkerType = MarkerType.Circle,
                Color = OxyColor.FromArgb(255, 0, 0, 255) // Màu xanh lam
            };

            foreach (var item in newsCountByDay)
            {
                var day = DateTime.ParseExact(item.Day, "yyyy-MM-dd", null).Day;
                lineSeries.Points.Add(new DataPoint(day, item.Count));
            }

            PlotModel.Series.Clear();
            PlotModel.Series.Add(lineSeries);
            PlotModel.InvalidatePlot(true);
        }

    }
}