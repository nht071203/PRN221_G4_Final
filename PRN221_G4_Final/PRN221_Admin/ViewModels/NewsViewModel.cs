using PRN221_BusinessLogic.Interface;
using PRN221_BusinessLogic.Service;
using PRN221_Models.Models;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace PRN221_Admin.ViewModels
{
    public class NewsViewModel : BaseViewModel
    {


        private ObservableCollection<News> _news;
        private ObservableCollection<CategoryNews> _categories;
        private CategoryNews _selectedCategoryNews;

        private string _selectedCategoryNewsName;
        private ObservableCollection<string> _listCategoryNewsName;
        private News _selectedNews;

        private readonly INewsService newsService;
        //xu ly image

        private FirebaseConfig _firebaseConfig;
        private string _imageUrl;

        public string ImageUrl
        {
            get => _imageUrl;
            set
            {
                _imageUrl = value;
                OnPropertyChanged(nameof(ImageUrl));
            }
        }

        public ObservableCollection<string> ListCategoryNewsName
        {
            get => _listCategoryNewsName;
            set
            {
                _listCategoryNewsName = value;
                OnPropertyChanged(nameof(ListCategoryNewsName));
            }
        }
        public ObservableCollection<News> News

        {
            get { return _news; }
            set
            {
                _news = value;
                OnPropertyChanged(nameof(News));

            }
        }

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
        public ICommand UploadImageCommand { get; set; }
        // public ICommand UploadAvatarCommand { get; set; }

        public NewsViewModel(INewsService accountService)
        {
            News = new ObservableCollection<News>();
            categoryNews = new ObservableCollection<CategoryNews>();
            ListCategoryNewsName = new ObservableCollection<string>();
            newsService = accountService;


            _firebaseConfig = new FirebaseConfig();
            UploadImageCommand = new RelayCommand(async (obj) => await UploadImage());
            AddNewsCommand = new RelayCommand(async (obj) => await AddNews(obj));
            RemoveNewsCommand = new RelayCommand(async (obj) => await DeleteNews(obj));
            EditNewsCommand = new RelayCommand(async (obj) => await EditNews(obj));
            ClearFormCommand = new RelayCommand(ClearForm);
            _ = LoadNews();


        }
        private DateTime? _createDate;
        public DateTime? CreateDate
        {
            get { return _createDate; }
            set
            {
                _createDate = value;
                OnPropertyChanged(nameof(CreateDate));
            }
        }

        private DateTime? _updateDate;
        public DateTime? UpdateDate
        {
            get { return _updateDate; }
            set
            {
                _updateDate = value;
                OnPropertyChanged(nameof(UpdateDate));
            }
        }

        public News SelectedNews
        {
            get { return _selectedNews;
              
            }
            set
            {
                _selectedNews = value;
                OnPropertyChanged(nameof(SelectedNews));
                if (SelectedNews != null)
                {
                    CreateDate = _selectedNews.CreatedAt;
                    UpdateDate = _selectedNews.UpdatedAt;
                    ImageUrl = _selectedNews?.ImageUrl;
                    OnPropertyChanged(nameof(ImageUrl));


                    foreach (var cat in categoryNews)
                    {
                        if (SelectedNews.CategoryNewsId == cat.CategoryNewsId)
                        {
                            SelectedCategoryNewsName = cat.CategoryNewsName;

                        }
                    }
                    OnPropertyChanged(nameof(SelectedCategoryNewsName));
                    IsClearButtonEnabled = _selectedNews != null;
                    IsAddButtonEnabled = false;
                    IsEditButtonEnable = true;
                    IsEditDateEnabled = true;
                    IsDeleteButtonEnabled = true;


                }
               // OnPropertyChanged(nameof(SelectedCategoryNewsName));
                OnPropertyChanged(nameof(SelectedNews));

            }
        }


        public string SelectedCategoryNewsName
        {

            get { return _selectedCategoryNewsName; }
            set
            {
                _selectedCategoryNewsName = value;
                OnPropertyChanged(nameof(SelectedCategoryNewsName));



            }
        }

        public async Task LoadNews()
        {
           

            var students = await newsService.GetAllNews();
            News.Clear();
            foreach (var student in students)
            {
                if (student.IsDeleted == false)
                {

                    News.Add(student);
                }

            }
            await LoadCategoryNews();
            await LoadCategoryNewsName();


        }

        public async Task LoadCategoryNewsName()
        {
            var students = await newsService.GetAllCategoryNews();
            ListCategoryNewsName.Clear();
            foreach (var student in students)
            {
                ListCategoryNewsName.Add(student.CategoryNewsName);

            }

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


        private async Task UploadImage()
        {
            // Use OpenFileDialog from Microsoft.Win32 (for WPF)
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            };

            // Show the dialog to select an image file
            if (openFileDialog.ShowDialog() == true)
            {
                // Get file path
                var filePath = openFileDialog.FileName;

                // Open a file stream to read the image
                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    // Call UploadToFirebase by passing the stream and the file name
                    var fileName = Path.GetFileName(filePath);
                    var imageUrl = await _firebaseConfig.UploadToFirebase(stream, fileName);

                    // Update ImageUrl with the returned URL
                    ImageUrl = imageUrl;
                }
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

            SelectedNews = new News
            {
                NewsId = 0, // Giả sử ID mặc định là 0 khi chưa chọn gì
                CategoryNewsId = 0,
                Title = string.Empty,
                Content = string.Empty,
                CreatedAt = SelectedNews.CreatedAt,
                UpdatedAt = SelectedNews.UpdatedAt,
                IsDeleted = false
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
        public async Task AddNews(object obj)
        {

            try
            {
                if (
                    string.IsNullOrWhiteSpace(SelectedNews.Title) ||
                    string.IsNullOrWhiteSpace(SelectedNews.Content) ||
                    string.IsNullOrWhiteSpace(SelectedNews.CreatedAt.ToString()))
                {
                    MessageBox.Show("Please enter enough your information!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                  
                }
                else if (SelectedNews.CreatedAt > DateTime.Now)
                {
                    MessageBox.Show("Create day must less or equal day now!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                else
                {
                    SelectedNews.IsDeleted = false;// Default phone number
                    SelectedNews.ImageUrl = ImageUrl;
                    SelectedNews.CreatedAt = CreateDate;
                    
                    foreach (var cat in categoryNews)
                    {
                        if (SelectedCategoryNewsName == cat.CategoryNewsName)
                        {
                            SelectedNews.CategoryNewsId = cat.CategoryNewsId;
                            break;
                        }
                    }

                    // Add the new expert account
                    await newsService.AddNews(SelectedNews);
                    await LoadNews();

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
                    
                    var newsEdit = await newsService.GetByIdNews(SelectedNews.NewsId);
                    if (newsEdit == null)
                    {
                       MessageBox.Show("The news of this id is not exist", "Id not exist", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(SelectedCategoryNewsName) ||
                        string.IsNullOrWhiteSpace(SelectedNews.Title) ||
                        string.IsNullOrWhiteSpace(SelectedNews.Content))
                        {
                           MessageBox.Show("Please enter enough your information!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }

                        else
                        {
                            SelectedNews.ImageUrl = ImageUrl;
                            SelectedNews.CreatedAt = CreateDate;
                            await LoadCategoryNews();
                            foreach (var cat in categoryNews)
                            {
                                if (SelectedCategoryNewsName == cat.CategoryNewsName)
                                {
                                    SelectedNews.CategoryNewsId = cat.CategoryNewsId;
                                    break;
                                }
                            }

                            // Add the new expert account
                            await newsService.UpdateNews(SelectedNews);
                            await LoadNews();

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
                var newsEdit = await newsService.GetByIdNews(SelectedNews.NewsId);
                if (newsEdit == null)
                {

                    System.Windows.MessageBox.Show("The news of this id is not exist", "Id not exist", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                SelectedNews.IsDeleted = true;
                SelectedNews.DeletedAt = DateTime.Now;
                await newsService.UpdateNews(SelectedNews);
                await LoadNews();

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Delete news has error", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
}
