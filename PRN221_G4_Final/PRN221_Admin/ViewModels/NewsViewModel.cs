using PRN221_BusinessLogic.Interface;
using PRN221_BusinessLogic.Service;
using PRN221_Models.Models;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace PRN221_Admin.ViewModels
{
    public class NewsViewModel : BaseViewModel
    {

        private ObservableCollection<News> _news;

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
        public ObservableCollection<News> News

        {
            get { return _news; }
            set
            {
                _news = value;
                OnPropertyChanged(nameof(News));

            }
        }
        //Command
        public ICommand RemoveNewsCommand { get; set; }
        public ICommand AddNewsCommand { get; set; }
        public ICommand ClearFormCommand { get; set; }
        public ICommand UploadImageCommand { get; set; }
       // public ICommand UploadAvatarCommand { get; set; }

        public NewsViewModel(INewsService accountService)
        {
            News = new ObservableCollection<News>();

            newsService = accountService;
            _firebaseConfig = new FirebaseConfig();
            UploadImageCommand = new RelayCommand(async (obj) => await UploadImage());
            _ = LoadNews();

        }
        private DateOnly? _createDate;
        public DateOnly? CreateDate
        {
            get { return _createDate; }
            set
            {
                _createDate = value;
                OnPropertyChanged(nameof(CreateDate));
            }
        }

        private DateOnly? _updateDate;
        public DateOnly? UpdateDate
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
            get { return _selectedNews; }
            set
            {
                _selectedNews = value;
                OnPropertyChanged(nameof(SelectedNews));
                if (SelectedNews != null)
                {

                    CreateDate = _selectedNews.CreatedAt;
                    UpdateDate = _selectedNews.UpdatedAt;
                }


            }
        }

        public async Task LoadNews()
        {

            /*var students = await expertService.GetByIdAccount(2);
            students.IsDeleted = true;
            await expertService.UpdateAccount(students);*/
            var students = await newsService.GetAllNews();
            News.Clear();
            foreach (var student in students)
            {
                if (student.IsDeleted == false)
                {

                    News.Add(student);
                }

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
            get => _isClearButtonEnabled;
            set
            {
                _isClearButtonEnabled = value;
                OnPropertyChanged(nameof(IsAddButtonEnabled));
            }
        }

        public bool IsDeleteButtonEnabled
        {
            get => _isClearButtonEnabled;
            set
            {
                _isClearButtonEnabled = value;
                OnPropertyChanged(nameof(IsDeleteButtonEnabled));
            }
        }

        private void ClearForm(object obj)
        {
            // Lưu lại giá trị của Gender trước khi xóa


            // Xóa tất cả các trường trên form ngoại trừ Gender
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
            IsAddButtonEnabled = true;
        }

    }
}
