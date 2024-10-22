using Microsoft.Identity.Client;
using PRN221_BusinessLogic.Interface;
using PRN221_BusinessLogic.Service;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;


namespace PRN221_Admin.ViewModels
{
    public class FarmerModelView : BaseViewModel
    {

        private ObservableCollection<Account> _expert;

        private Account _selectedExpert;

        private readonly IAccountService accountService;
        //public ObservableCollection<Account> Accounts { get; set; } = new ObservableCollection<Account>();

        public ObservableCollection<Account> Farmers
        {
            get { return _expert; }
            set
            {
                _expert = value;
                OnPropertyChanged(nameof(Farmers));

            }
        }


        public ICommand AddFarmerCommand { get; set; }
        public ICommand DeleteFarmerBtn { get; set; }
        public ICommand UpdateFarmerBtn { get; set; }

        public ICommand ClearToAdd { get; set; }


        public FarmerModelView(IAccountService accountService)
        {
            Farmers = new ObservableCollection<Account>();

            this.accountService = accountService;



            _firebaseConfig = new FirebaseConfig();

            _fireBaseAvatar = new FirebaseConfig();
            UploadAvatarFarmerCommand = new RelayCommand(async (obj) => await UploadAvatar());



            _ = LoadFarmers();

            DeleteFarmerBtn = new RelayCommand(async (obj) => await DeleteMember(obj), CanExecute2);
            AddFarmerCommand = new RelayCommand(async (obj) => await AddFarmer(obj), CanExecute);
            ClearToAdd = new RelayCommand(ClearFields, CanExecute2);

        }


        private FirebaseConfig _firebaseConfig;
        private FirebaseConfig _fireBaseAvatar;
        private string _imageUrl;
        private string _avatar;

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




        private async Task UploadAvatar()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var filePath = openFileDialog.FileName;

                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    var fileName = Path.GetFileName(filePath);
                    var imageUrl = await _firebaseConfig.UploadToFirebase(stream, fileName);

                    AvatarURL = imageUrl;
                }
            }
        }

        public ICommand UploadAvatarFarmerCommand { get; set; }





        private Account _selectedFarmer;
        public Account SelectedFarmers
        {
            get => _selectedFarmer;
            set
            {
                _selectedFarmer = value;
                OnPropertyChanged(nameof(SelectedFarmers));

                //OnPropertyChanged(nameof(IsMale));
                //OnPropertyChanged(nameof(IsFemale));

                if (_selectedFarmer != null)
                {
                    AvatarURL = _selectedFarmer?.Avatar;
                    accountId = _selectedFarmer.AccountId;
                    FullName = _selectedFarmer.FullName;
                    Gender = _selectedFarmer.Gender;
                    Username = _selectedFarmer.Username;
                    Email = _selectedFarmer.Email;
                    Phone = _selectedFarmer.Phone;
                    DateofbirthInfo = _selectedFarmer.DateOfBirth;
                    Address = _selectedFarmer.Address;


                }
            }
        }
        public async Task LoadFarmers()
        {

            var students = await accountService.GetListAccountByRoleId(1);
            Farmers.Clear();
            foreach (var student in students)
            {
                Farmers.Add(student);

            }

        }



        private void ClearFields(object obj)
        {
            accountId = 0;
            Username = string.Empty;
            FullName = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
            Address = string.Empty;
            Gender = string.Empty;
            SelectedFarmers = null;
            OnPropertyChanged(nameof(IsMale));
            OnPropertyChanged(nameof(IsFemale));
        }

        private string _gender;
        public string Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender));
                OnPropertyChanged(nameof(IsMale)); // cập nhật IsMale và IsFemale khi Gender thay đổi
                OnPropertyChanged(nameof(IsFemale));
            }
        }

        public bool IsMale
        {
            get => Gender == "Male"; // kiểm tra giá trị Gender
            set
            {
                if (value) Gender = "Male"; // nếu IsMale là true thì đặt Gender là "Male"
            }
        }

        public bool IsFemale
        {
            get => Gender == "Female"; // kiểm tra giá trị Gender
            set
            {
                if (value) Gender = "Female"; // nếu IsFemale là true thì đặt Gender là "Female"
            }
        }


        private bool CanExecute(object obj)
        {
            return SelectedFarmers == null;

        }



        private bool CanExecute2(object obj)
        {
            return SelectedFarmers != null;

        }


        private string _Username;
        public string Username
        {
            get { return _Username; }
            set
            {
                _Username = value;
                OnPropertyChanged(nameof(Username));
            }
        }


        private int _accountId;
        public int accountId
        {
            get { return _accountId; }
            set
            {
                _accountId = value;
                OnPropertyChanged(nameof(accountId));
            }
        }


        private string _FullName;
        public string FullName
        {
            get { return _FullName; }
            set
            {
                _FullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }

        private DateOnly? _dateofbirthInfo;
        public DateOnly? DateofbirthInfo
        {
            get { return _dateofbirthInfo; }
            set
            {
                _dateofbirthInfo = value;
                OnPropertyChanged(nameof(DateofbirthInfo));
            }
        }

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set
            {
                _Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        private string _Phone;
        public string Phone
        {
            get { return _Phone; }
            set
            {
                _Phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }
        private string _Address;
        public string Address
        {
            get { return _Address; }
            set
            {
                _Address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        private async Task AddFarmer(object parameter)
        {

            if (string.IsNullOrWhiteSpace(Username))
            {
                MessageBox.Show("Username không thể trống!");
                return;
            }

            if (string.IsNullOrWhiteSpace(FullName))
            {
                MessageBox.Show("Full Name không thể trống!");
                return;
            }


            if (string.IsNullOrWhiteSpace(FullName) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Phone)
                || string.IsNullOrWhiteSpace(Address))
            {
                MessageBox.Show("Please fill!");
                return;
            }


            var existingAccount = await accountService.GetByUsername(Username);
            if (existingAccount != null)
            {
                MessageBox.Show("Username is exist!");
                return;
            }


            var existingAccountID = await accountService.GetByIdAccount(accountId);
            if (existingAccountID != null)
            {
                MessageBox.Show("ID is exist!");
                return;
            }

            if (string.IsNullOrWhiteSpace(AvatarURL))
            {
                await UploadAvatar();
            }


            string gender = IsMale ? "Male" : "Female";
            var st = new Account
            {
                Avatar = AvatarURL,
                Username = Username,
                FullName = FullName,
                Email = Email,
                Phone = Phone,
                Address = Address,
                Gender = IsMale ? "Male" : "Female",
                Password = "123",
                RoleId = 1,
                IsDeleted = false,
            };

            await accountService.AddAccount(st);
            Farmers.Add(st);

            Username = FullName = Email = Phone = Address = "";
            AvatarURL = null;
            OnPropertyChanged(nameof(IsMale));
            OnPropertyChanged(nameof(IsFemale));
            await LoadFarmers();
        }



        public async Task UpdateMember(object obj)
        {
            try
            {
                var existingMember = await accountService.GetByIdAccount(SelectedFarmers.AccountId);

                if (existingMember == null)
                {
                    System.Windows.MessageBox.Show("Can not find farmer.", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                await accountService.UpdateAccount(existingMember);
                await LoadFarmers();
                System.Windows.MessageBox.Show("Update successful!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Delete fail", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task DeleteMember(object obj)
        {
            try
            {
                var existingMember = await accountService.GetByIdAccount(SelectedFarmers.AccountId);

                if (existingMember == null)
                {
                    System.Windows.MessageBox.Show("Can not find farmer.", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var result = System.Windows.MessageBox.Show("Are you sure?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    existingMember.IsDeleted = true;
                    await accountService.DeleteAccount(existingMember);
                    await LoadFarmers();
                    System.Windows.MessageBox.Show("Delete successful!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    System.Windows.MessageBox.Show("Cancel delete action.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Delete fail", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



    }
}


