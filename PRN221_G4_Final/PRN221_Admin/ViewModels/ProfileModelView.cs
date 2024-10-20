using PRN221_BusinessLogic.Interface;
using PRN221_BusinessLogic.Service;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PRN221_Admin.ViewModels
{
    public class ProfileModelView : BaseViewModel
    {


        private ObservableCollection<Account> _admin;
        private readonly IAccountService accountService;


        public ObservableCollection<Account> Farmers
        {
            get { return _admin; }
            set
            {
                _admin = value;
                OnPropertyChanged(nameof(Farmers));

            }
        }



        private string _username;
        private string _fullname;
        private string _password;
        private string _email;
        private string _phone;
        private string _dateOfBirth;
        private string _gender; 
        private string _address;

        private string _facebookId;
        private string _shortBio;


        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Fullname
        {
            get => _fullname;
            set
            {
                _fullname = value;
                OnPropertyChanged(nameof(Fullname));
            }
        }



 

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }



        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }
        public string DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged(nameof(DateOfBirth));
            }
        }

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
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        public string FacebookId
        {
            get => _facebookId;
            set
            {
                _facebookId = value;
                OnPropertyChanged(nameof(FacebookId));
            }
        }
        public string ShortBio
        {
            get => _shortBio;
            set
            {
                _shortBio = value;
                OnPropertyChanged(nameof(ShortBio));
            }
        }




        public ProfileModelView(IAccountService accountService)
        {
            Farmers = new ObservableCollection<Account>();

            this.accountService = accountService;
            LoadAdminInfo("expert_user4");

            UpdateAdminBtn = new RelayCommand(async (obj) => await UpdateAdmin(obj));


        }

        public async Task LoadAdminInfo(string username)
        {
            var account = await accountService.GetByUsername(username);
            if (account != null)
            {
                Username = account.Username;
                Fullname = account.FullName;
                Email = account.Email;
                Phone = account.Phone;
             Gender = account.Gender;
                Password = account.Password;
                Address = account.Address;
                FacebookId = account.FacebookId;
                ShortBio = account.ShortBio;


                Debug.WriteLine("shortttt..." + ShortBio);
                Debug.WriteLine("Passss..." + Password);
            }
        }



        public ICommand UpdateAdminBtn { get; set; }

        public async Task UpdateAdmin(object obj)
        {
            try
            {
                // Lấy thông tin tài khoản hiện tại
                var existingMember = await accountService.GetByUsername(Username);

                if (existingMember == null)
                {
                    System.Windows.MessageBox.Show("Cannot find account.", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Cập nhật thông tin tài khoản với thông tin từ ViewModel
                existingMember.FullName = Fullname;
                existingMember.Email = Email;
                existingMember.Phone = Phone;
                existingMember.Gender = Gender;
                existingMember.Password = Password; // Cẩn thận với việc lưu mật khẩu
                existingMember.Address = Address;
                existingMember.FacebookId = FacebookId;
                existingMember.ShortBio = ShortBio;

                // Gọi phương thức cập nhật tài khoản
                await accountService.UpdateAccount(existingMember);

            
                await LoadAdminInfo(Username);

            
                System.Windows.MessageBox.Show("Update successful!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Update failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
