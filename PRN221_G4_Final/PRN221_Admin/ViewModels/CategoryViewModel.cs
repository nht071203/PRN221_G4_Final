﻿using PRN221_BusinessLogic.Interface;
using PRN221_BusinessLogic.Service;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace PRN221_Admin.ViewModels
{
    public class CategoryViewModel:BaseViewModel
    {

        private ObservableCollection<CategoryNews> _categories;
        private CategoryNews _selectedCategoryNews;

        private readonly INewsService newsService;
        //xu ly image




        public ObservableCollection<CategoryNews> categoryNews

        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(categoryNews));

            }
        }
        //Command
        public ICommand RemoveNewsCommand { get; set; }
        public ICommand AddNewsCommand { get; set; }
        public ICommand EditNewsCommand { get; set; }
        public ICommand ClearFormCommand { get; set; }
        // public ICommand UploadAvatarCommand { get; set; }

        public CategoryViewModel(INewsService accountService)
        {
            categoryNews = new ObservableCollection<CategoryNews>();
            newsService = accountService;
            AddNewsCommand = new RelayCommand(async (obj) => await AddCat(obj));
            RemoveNewsCommand = new RelayCommand(async (obj) => await DeleteNews(obj));
            EditNewsCommand = new RelayCommand(async (obj) => await EditNews(obj));
            ClearFormCommand = new RelayCommand(ClearForm);
            _ =LoadCategoryNews();


        }


        public async Task LoadCategoryNews()
        {
            var students = await newsService.GetAllCategoryNews();
            categoryNews.Clear();
            foreach (var student in students)
            {
                categoryNews.Add(student);

            }

        }
        public CategoryNews SelectedNews
        {
            get
            {
                return _selectedCategoryNews;

            }
            set
            {
                _selectedCategoryNews = value;
                OnPropertyChanged(nameof(SelectedNews));
                if (SelectedNews != null)
                {
         
                    IsClearButtonEnabled = _selectedCategoryNews != null;
                    IsAddButtonEnabled = false;
                    IsEditButtonEnable = true;
                    IsEditDateEnabled = true;
                    IsDeleteButtonEnabled = true;


                }
                // OnPropertyChanged(nameof(SelectedCategoryNewsName));
                OnPropertyChanged(nameof(SelectedNews));

            }
        }


        private bool _isClearButtonEnabled;
        private bool _isAddButtonEnabled;
        private bool _isDeleteButtonEnabled;
        private bool _isEditButtonEnabled;
        private bool _isEditDateEnabled;

        public bool IsClearButtonEnabled
        {
            get => _isClearButtonEnabled;
            set
            {
                _isClearButtonEnabled = value;
                OnPropertyChanged(nameof(IsClearButtonEnabled));
            }
        }

        public bool IsAddButtonEnabled
        {
            get => _isAddButtonEnabled;
            set
            {
                _isAddButtonEnabled = value;
                OnPropertyChanged(nameof(IsAddButtonEnabled));
            }
        }
        public bool IsEditButtonEnable
        {
            get => _isEditButtonEnabled;
            set
            {
                _isEditButtonEnabled = value;
                OnPropertyChanged(nameof(IsEditButtonEnable));
            }
        }

        public bool IsDeleteButtonEnabled
        {
            get => _isDeleteButtonEnabled;
            set
            {
                _isDeleteButtonEnabled = value;
                OnPropertyChanged(nameof(IsDeleteButtonEnabled));
            }
        }

        public bool IsEditDateEnabled
        {
            get => _isEditDateEnabled;
            set
            {
                _isEditDateEnabled = value;
                OnPropertyChanged(nameof(IsEditDateEnabled));
            }
        }
        private void ClearForm(object obj)
        {

            SelectedNews = new CategoryNews
            {
                CategoryNewsId = 0, // Giả sử ID mặc định là 0 khi chưa chọn gì

                CategoryNewsName = string.Empty,
                CategoryNewsDescription = string.Empty,
            };

            // Cập nhật trạng thái các thuộc tính liên quan đến UI
            OnPropertyChanged(nameof(SelectedNews));

            IsClearButtonEnabled = false;
            IsDeleteButtonEnabled = false;
            IsEditButtonEnable = false;
            IsEditDateEnabled = false;
            IsAddButtonEnabled = true;
        }

        //crud
        public async Task AddCat(object obj)
        {

            try
            {
                if (
                    string.IsNullOrWhiteSpace(SelectedNews.CategoryNewsName) ||
                    string.IsNullOrWhiteSpace(SelectedNews.CategoryNewsDescription))
                  
                {
                    MessageBox.Show("Please enter enough your information!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
               
                else
                {

                    // Add the new expert account
                   await newsService.AddCatNews(SelectedNews);
                    await LoadCategoryNews();

                }


                //IsDeleteButtonEnabled = false;

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Add new news has error", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public async Task EditNews(object obj)
        {
            if (SelectedNews != null)
            {
                try
                {

                    var newsEdit = await newsService.GetCategoryNewsById(SelectedNews.CategoryNewsId);
                    if (newsEdit == null)
                    {
                        MessageBox.Show("The category news of this id is not exist", "Id not exist", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(SelectedNews.CategoryNewsName) ||
                        string.IsNullOrWhiteSpace(SelectedNews.CategoryNewsDescription)
                       )
                        {
                            MessageBox.Show("Please enter enough your information!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }

                        else
                        {
                           
                            await newsService.UpdateCatNews(SelectedNews);
                            await LoadCategoryNews();

                        }

                    }


                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Edit new news has error", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

        }

        public async Task DeleteNews(object obj)
        {
            try
            {
                var newsEdit = await newsService.GetCategoryNewsById(SelectedNews.CategoryNewsId);
                if (newsEdit == null)
                {

                    System.Windows.MessageBox.Show("The news of this id is not exist", "Id not exist", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    await newsService.DeleteCatNews(SelectedNews.CategoryNewsId);
                    await LoadCategoryNews();
                }

               
               

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Delete news has error", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
}
