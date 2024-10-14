//using PRN221_BusinessLogic.Interface;
//using PRN221_Models.Models;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
////using System.Windows.Forms;
//using System.Windows.Input;
//namespace PRN221_Admin.ViewModels
//{
//   public class FarmerModelView : BaseViewModel
//    {
//        public FarmerModelView() { }

//        private readonly IAccountService accountService;
//        public ObservableCollection<Account> Farmer { get; set; } = new ObservableCollection<Account>();
//        public async Task LoadFarmers()
//        {
//            var members = await accountService.GetListAccountByRoleId(1);
//            Farmer.Clear();
//            foreach (var member in members)
//            {

//                Farmer.Add(member);
//            }
//        }

//        public ICommand AddFarmerCommand { get; set; }
//        public ICommand DeleteFarmerBtn { get; set; }

//        public FarmerModelView(IAccountService accountService)
//        {
//            Farmer = new ObservableCollection<Account>();
//            this.accountService = accountService;
//            _ = LoadFarmers();

//            DeleteFarmerBtn = new RelayCommand(async (obj) => await DeleteMember(obj), CanExecute);
//        }

//        private Account _selectedFarmer;
//        public Account SelectedFarmer
//        {
//            get => _selectedFarmer;
//            set
//            {
//                _selectedFarmer = value;
//                OnPropertyChanged(nameof(SelectedFarmer));
//                OnPropertyChanged(nameof(IsMale));
//                OnPropertyChanged(nameof(IsFemale));
//            }
//        }


//        public bool IsMale
//        {
//            get => SelectedFarmer != null && SelectedFarmer.Gender == "Male";
//            set
//            {
//                if (SelectedFarmer != null)
//                {
//                    SelectedFarmer.Gender = value ? "Male" : SelectedFarmer.Gender;
//                    OnPropertyChanged(nameof(IsMale));
//                    OnPropertyChanged(nameof(IsFemale));
//                }
//            }
//        }

//        public bool IsFemale
//        {
//            get => SelectedFarmer != null && SelectedFarmer.Gender == "Female";
//            set
//            {
//                if (SelectedFarmer != null)
//                {
//                    SelectedFarmer.Gender = value ? "Female" : SelectedFarmer.Gender;
//                    OnPropertyChanged(nameof(IsMale));
//                    OnPropertyChanged(nameof(IsFemale));
//                }
//            }
//        }
//        private bool CanExecute(object obj)
//        {
//            return SelectedFarmer != null;
//        }


//        public async Task UpdateMember(object obj)
//        {
//            try
//            {
//                var existingMember = await accountService.GetByIdAccount(SelectedFarmer.AccountId);

//                if (existingMember == null)
//                {
//                    System.Windows.MessageBox.Show("Can not find farmer.", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
//                    return;
//                }

//                var result = System.Windows.MessageBox.Show("Are you sure?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);

//                if (result == MessageBoxResult.Yes)
//                {
//                    //existingMember.IsDeleted = true;
//                    await accountService.DeleteAccount(existingMember);
//                    await LoadFarmers();
//                    System.Windows.MessageBox.Show("Delete successful!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
//                }
//                else
//                {
//                    System.Windows.MessageBox.Show("Cancel delete action.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
//                }
//            }
//            catch (Exception ex)
//            {
//                System.Windows.MessageBox.Show("Delete fail", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//        }

//        public async Task DeleteMember(object obj)
//        {
//            try
//            {
//                var existingMember = await accountService.GetByIdAccount(SelectedFarmer.AccountId);

//                if (existingMember == null)
//                {
//                    System.Windows.MessageBox.Show("Can not find farmer.", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
//                    return;
//                }

//                var result = System.Windows.MessageBox.Show("Are you sure?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);

//                if (result == MessageBoxResult.Yes)
//                {
//                    existingMember.IsDeleted = true;
//                    await accountService.DeleteAccount(existingMember);
//                    await LoadFarmers();
//                    System.Windows.MessageBox.Show("Delete successful!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
//                }
//                else
//                {
//                    System.Windows.MessageBox.Show("Cancel delete action.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
//                }
//            }
//            catch (Exception ex)
//            {
//                System.Windows.MessageBox.Show("Delete fail", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//        }



//    }
//}




using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PRN221_BusinessLogic.Interface;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Forms;
using System.Windows.Input;
namespace PRN221_Admin.ViewModels
{
    public class FarmerModelView : BaseViewModel
    {
        public FarmerModelView() { }

        private readonly IAccountService accountService;
        public ObservableCollection<Account> Farmer { get; set; } = new ObservableCollection<Account>();
        public async Task LoadFarmers()
        {
            var members = await accountService.GetListAccountByRoleId(1);
            Farmer.Clear();
            foreach (var member in members)
            {

                Farmer.Add(member);
            }
        }

        public ICommand AddFarmerCommand { get; set; }
        public ICommand DeleteFarmerBtn { get; set; }
        public ICommand UpdateFarmerBtn { get; set; }

        public ICommand ClearToAdd { get; set; }

        public FarmerModelView(IAccountService accountService)
        {
            Farmer = new ObservableCollection<Account>();
            this.accountService = accountService;
            _ = LoadFarmers();

            DeleteFarmerBtn = new RelayCommand(async (obj) => await DeleteMember(obj),CanExecute2);
            AddFarmerCommand = new RelayCommand(async (obj) => await AddFarmer(obj), CanExecute);
            ClearToAdd = new RelayCommand( ClearFields, CanExecute2);

        }

        private Account _selectedFarmer;
        public Account SelectedFarmer
        {
            get => _selectedFarmer;
            set
            {
                _selectedFarmer = value;
                OnPropertyChanged(nameof(SelectedFarmer));

                //OnPropertyChanged(nameof(IsMale));
                //OnPropertyChanged(nameof(IsFemale));

                if (_selectedFarmer != null)
                {
                    accountId = _selectedFarmer.AccountId;
                    FullName = _selectedFarmer.FullName;
                    Username = _selectedFarmer.Username;
                    Email = _selectedFarmer.Email;
                    Phone = _selectedFarmer.Phone;
                    Address = _selectedFarmer.Address;
              

                }
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
            SelectedFarmer = null;
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
            return SelectedFarmer == null;
       
        }


 
        private bool CanExecute2(object obj)
        {
            return SelectedFarmer != null;
       
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

        //private DateOnly? _dateofbirthInfo;
        //public DateOnly? dateofbirthInfo
        //{
        //    get { return _dateofbirthInfo; }
        //    set
        //    {
        //        _dateofbirthInfo = value;
        //        OnPropertyChanged(nameof(dateofbirthInfo));
        //    }
        //}

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

            string gender = IsMale ? "Male" : "Female";
            var st = new Account
            {
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
            Farmer.Add(st);

            Username = FullName = Email = Phone = Address = "";
            OnPropertyChanged(nameof(IsMale));
            OnPropertyChanged(nameof(IsFemale));
            await LoadFarmers();
        }

   

        public async Task UpdateMember(object obj)
        {
            try
            {
                var existingMember = await accountService.GetByIdAccount(SelectedFarmer.AccountId);

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
                var existingMember = await accountService.GetByIdAccount(SelectedFarmer.AccountId);

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
