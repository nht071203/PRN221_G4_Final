using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using PRN221_BusinessLogic.Interface;
using PRN221_DataAccess.DAOs;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PRN221_Admin.ViewModels
{
    public class ThongKeModelView : BaseViewModel
    {
        public PlotModel PlotModel { get; set; }
        private readonly INewsService newsService;
        private readonly IAccountService accountService;
        private readonly IPostService postService;

        public ThongKeModelView(INewsService newsService, IAccountService accountService, IPostService postService)
        {
            this.newsService = newsService;
            this.accountService = accountService;
            this.postService = postService;

            PlotModel = new PlotModel { Title = "Post Chart" };
            PlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Month" });
            PlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Posts" });
            LoadDataCommand = new RelayCommand(async (obj) => await LoadDataPo());
            LoadDataYearMonth = new RelayCommand(async (obj) => await LoadDataPoDay());
            LoadData();
        }

        public ICommand LoadDataCommand { get; set; }

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



        private async Task LoadData()
        {
            TotalNewsCount = await newsService.GetTotalNewsCount();
            TotalFarmerCount = await accountService.GetTotalFarmerService();
            TotalExpertCount = await accountService.GetTotalExpertService();

            TopFarmer = await postService.FarmerWithMostPosts() ?? new Account();
            TopExpert = await postService.ExpertWithMostPosts() ?? new Account();

        }

        private async Task LoadDataPo()
        {
            int currentYear = Year > 0 ? Year : DateTime.Now.Year;
            // Sử dụng năm người dùng nhập vào, nếu không có thì dùng năm hiện tại
            var newsCountByMonth = await newsService.GetNewsCountByMonth(currentYear);


            if (Year < 2020 || Year > currentYear)
            {
                // Thông báo cho người dùng nếu năm không hợp lệ
                MessageBox.Show("Year invalid!");
                return;
            }

            var lineSeries = new LineSeries
            {
                Title = "Post in month",
                MarkerType = MarkerType.Diamond,
                Color = OxyColor.FromArgb(255, 255, 0, 0)
            };

            foreach (var item in newsCountByMonth)
            {
                try
                {
                    var month = DateTime.ParseExact(item.Month, "yyyy-MM", null).Month;
                    lineSeries.Points.Add(new DataPoint(month, item.Count));
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Lỗi định dạng ngày tháng: {item.Month}. Chi tiết: {ex.Message}");
                }
            }
            PlotModel.Series.Clear();
            PlotModel.Series.Add(lineSeries);
            PlotModel.InvalidatePlot(true);
        }




        private async Task LoadDataPoDay()
        {
            int currentYear = Year > 0 ? Year : DateTime.Now.Year;
            int selectedYear = Year > 0 ? Year : DateTime.Now.Year;
            int selectedMonth = Month > 0 ? Month : DateTime.Now.Month;

            var newsCountByDay = await newsService.GetNewsCountByDay(selectedYear, selectedMonth);

            if (Year < 2020 || Year > currentYear && Month < 1 || Month >12)
            {
                // Thông báo cho người dùng nếu năm không hợp lệ
                MessageBox.Show("Input invalid");
                return;
            }


            var lineSeries = new LineSeries
            {
                Title = "Post in day",
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


    }
}