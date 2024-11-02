﻿using OxyPlot;
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

            LoadData();
        }

        private async void LoadData()
        {
            var newsCountByMonth = await newsService.GetNewsCountByMonth();
            TotalNewsCount = await newsService.GetTotalNewsCount();
            TotalFarmerCount = await accountService.GetTotalFarmerService();
            TotalExpertCount = await accountService.GetTotalExpertService();

            TopFarmer = await postService.FarmerWithMostPosts() ?? new Account();
            TopExpert = await postService.ExpertWithMostPosts() ?? new Account();

            var lineSeries = new LineSeries
            {
                Title = "Số bài post trong tháng",
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