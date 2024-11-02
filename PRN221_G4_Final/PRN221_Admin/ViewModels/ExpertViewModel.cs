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
    public class ExpertViewModel : BaseViewModel
    {
        private ObservableCollection<Account> _experts;

        private Account _selectedExpert;

        private readonly IAccountService expertService;
        // xu li image
        private FirebaseConfig _firebaseConfig;
        private FirebaseConfig _fireBaseAvatar;
        private FirebaseConfig _fireBaseEducation;
        private string _imageUrl;
        private string _avatar;
        private string _education;

        private DateTime? _birthday;
        public DateTime? Birthday
        {
            get { return _birthday; }
            set
            {
                _birthday = value;
                OnPropertyChanged(nameof(Birthday));
            }
        }
        public string ImageUrl
        {
            get => _imageUrl;
            set
            {
                _imageUrl = value;
                OnPropertyChanged(nameof(ImageUrl));
            }
        }
        public string AvatarURL
        {
            get => _avatar;
            set
            {
                _avatar = value;
                OnPropertyChanged(nameof(AvatarURL));
            }
        }
        public string Education
        {
            get => _education;
            set
            {
                _education = value;
                OnPropertyChanged(nameof(Education));
            }
        }


        private async Task UploadDegree()
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

        private async Task UploadAvatar()
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
                    AvatarURL = imageUrl;
                }
            }
        }
        private async Task UploadEducation()
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
                    Education = imageUrl;
                }
            }
        }

        public ObservableCollection<Account> Experts
        {
            get { return _experts; }
            set
            {
                _experts = value;
                OnPropertyChanged(nameof(Experts));
                IsAddButtonEnabled = false;
                IsDeleteButtonEnabled = false;
            }
        }




        //command
        public ICommand RemoveExpertCommand { get; set; }
        public ICommand AddExpertCommand { get; set; }
        public ICommand ClearFormCommand { get; set; }
        public ICommand UploadDegreeCommand { get; set; }
        public ICommand UploadAvatarCommand { get; set; }
        public ICommand UploadEducationCommand { get; set; }

        public ExpertViewModel(IAccountService accountService)
        {
            Experts = new ObservableCollection<Account>();
            // FarmerAccount = new ObservableCollection<Account>();
            expertService = accountService;
            //image
            _firebaseConfig = new FirebaseConfig();
            UploadDegreeCommand = new RelayCommand(async (obj) => await UploadDegree());
            _fireBaseAvatar = new FirebaseConfig();
            UploadAvatarCommand = new RelayCommand(async (obj) => await UploadAvatar());
            _fireBaseEducation = new FirebaseConfig();
            UploadEducationCommand = new RelayCommand(async (obj) => await UploadEducation());
            //command of delete, add and clear

            RemoveExpertCommand = new RelayCommand(async (obj) => await DeleteExpert(obj));
            AddExpertCommand = new RelayCommand(async (obj) => await AddExpert(obj));
            ClearFormCommand = new RelayCommand(ClearForm);
            _ = LoadAccounts();
            //  _ = LoadFarmer();


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
        public Account SelectedExpert
        {
            get { return _selectedExpert; }
            set
            {
                _selectedExpert = value;
                OnPropertyChanged(nameof(SelectedExpert));
                ImageUrl = _selectedExpert?.DegreeUrl;
                AvatarURL = _selectedExpert?.Avatar;
                Education = _selectedExpert?.EducationUrl;
                Birthday = _selectedExpert?.DateOfBirth;
                OnPropertyChanged(nameof(IsMaleChecked));  // Update Male selection
                OnPropertyChanged(nameof(IsFemaleChecked));
                OnPropertyChanged(nameof(ImageUrl));// Update Female selection
                IsClearButtonEnabled = _selectedExpert != null;
                IsAddButtonEnabled = false;
                IsDeleteButtonEnabled = true;

            }
        }
        public async Task LoadAccounts()
        {


            var students = await expertService.GetListAccountByRoleId(2);
            Experts.Clear();
            foreach (var student in students)
            {
                if (student.IsDeleted == false)
                {

                    Experts.Add(student);
                }

            }
            IsDeleteButtonEnabled = false;
            IsAddButtonEnabled = false;

        }

        private async Task<IEnumerable<Account>> getAllAccount()
        {
            return await expertService.GetListAllAccount();
        }

        private async Task<bool> CheckUsernameIsExist(string username)
        {
            var accounts = await getAllAccount();
            return accounts.Any(account => username.Equals(account.Username));
        }

        public async Task AddExpert(object obj)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SelectedExpert.Username) ||
                    string.IsNullOrWhiteSpace(SelectedExpert.FullName) ||
                    string.IsNullOrWhiteSpace(SelectedExpert.Email) ||
                    string.IsNullOrWhiteSpace(SelectedExpert.Phone) ||
                    string.IsNullOrWhiteSpace(SelectedExpert.Password) ||
                    string.IsNullOrWhiteSpace(SelectedExpert.Address) ||
                    string.IsNullOrWhiteSpace(SelectedExpert.Major)


                    )
                {
                    System.Windows.MessageBox.Show("Please enter enough your information!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    //return; // Ngừng thực hiện phương thức nếu không có đủ thông tin
                }
                else if (await CheckUsernameIsExist(SelectedExpert.Username))
                {
                    System.Windows.MessageBox.Show("This username is already existed!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                else
                {
                    SelectedExpert.IsDeleted = false;// Default phone number
                    SelectedExpert.DegreeUrl = ImageUrl;
                    SelectedExpert.Avatar = AvatarURL;
                    SelectedExpert.EducationUrl = Education;
                    SelectedExpert.DateOfBirth = Birthday;
                    // Add the new expert account
                    await expertService.AddAccount(SelectedExpert);
                    await LoadAccounts();

                }

               
                //IsDeleteButtonEnabled = false;

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Add new expert has error", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        public async Task DeleteExpert(object obj)
        {
            try
            {
                var expert = await expertService.GetByIdAccount(SelectedExpert.AccountId);
                if (expert == null)
                {
                    System.Windows.MessageBox.Show("The expert of this id is not exist", "Id not exist", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;

                }
                expert.IsDeleted = true;
                await expertService.UpdateAccount(expert);

                await LoadAccounts();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Delete this expert has error", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public bool IsMaleChecked
        {
            get => SelectedExpert?.Gender == "Male";
            set
            {
                if (value && SelectedExpert != null)
                {
                    SelectedExpert.Gender = "Male";
                    OnPropertyChanged(nameof(SelectedExpert)); // Thông báo khi thay đổi SelectedStaff
                }
            }
        }

        // Thuộc tính kiểm tra nếu giới tính là Nữ
        public bool IsFemaleChecked
        {
            get => SelectedExpert?.Gender == "Female";
            set
            {
                if (value && SelectedExpert != null)
                {
                    SelectedExpert.Gender = "Female";
                    OnPropertyChanged(nameof(SelectedExpert)); // Thông báo khi thay đổi SelectedStaff
                }
            }
        }


        //private bool CanExecute(object obj)
        //{

        //    return SelectedExpert != null;

        //}

        private void ClearForm(object obj)
        {
            // Lưu lại giá trị của Gender trước khi xóa
            var currentGender = SelectedExpert.Gender;

            // Xóa tất cả các trường trên form ngoại trừ Gender
            SelectedExpert = new Account
            {
                AccountId = 0, // Giả sử ID mặc định là 0 khi chưa chọn gì
                Username = string.Empty,
                FullName = string.Empty,
                Email = string.Empty,
                Phone = string.Empty,
                Gender = currentGender, // Giữ lại giá trị Gender
                Address = string.Empty,
                Major = string.Empty,
                Password = string.Empty,
                ShortBio = string.Empty,
                YearOfExperience = 0,

                // Xóa mật khẩu
                RoleId = 2 // Giữ RoleId là Expert (2) nếu cần
            };

            // Cập nhật trạng thái các thuộc tính liên quan đến UI
            OnPropertyChanged(nameof(SelectedExpert));
            OnPropertyChanged(nameof(IsMaleChecked));
            OnPropertyChanged(nameof(IsFemaleChecked));
            IsClearButtonEnabled = false;
            IsDeleteButtonEnabled = false;
            IsAddButtonEnabled = true;
        }
    }
}

