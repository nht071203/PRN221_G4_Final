using PRN221_BusinessLogic.Interface;
using PRN221_BusinessLogic.Service;
using PRN221_DataAccess;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Composition;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PRN221_Admin.Views;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Extensions.DependencyInjection;

namespace PRN221_Admin.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _username;
        private string _password;
        private string _errorMessage;
        private ObservableCollection<Account> _accounts;
        private readonly IAccountService accountService;
        private readonly INewsService newsService;
        private ExpertViewModel _ExpertViewModel;
        public NewsViewModel _NewsViewModel;
        private FarmerModelView _farmerViewModel;
        private LoginViewModel _loginViewModel;
        private ProfileModelView _profileViewModel;

        public ObservableCollection<Account> Accounts
        {
            get { return _accounts; }
            set
            {
                _accounts = value;
                OnPropertyChanged(nameof(Accounts));
            }
        }
        //command
        //-> Commands
        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }
        public ICommand isCloseCommand { get; }

        public async Task LoadAccounts()
        {
            var students = await accountService.GetListAccountByRoleId(3);
            Accounts.Clear();
            foreach (var student in students)
            {
                if (student.IsDeleted == false)
                {

                    Accounts.Add(student);
                }

            }

        }

        public string Username
        {
            get
            {
                return _username;
            }

            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public String Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }

            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        private bool checkLogin(string user, string pass)
        {

            foreach (var acc in Accounts)
            {
                if (user == acc.Username && pass == acc.Password)
                {
                    return true;  // Return true immediately if a match is found
                }
            }
            return false;  // Return false if no match is found
        }
        private bool CanExecute(object obj)
        {
            return !string.IsNullOrWhiteSpace(Username) && Username.Length >= 3 &&
                   Password != null && Password.Length >= 3;
        }
        private void ExcuteCloseLogin(object obj)
        {
            if (obj is System.Windows.Window window)
            {
                window.Close(); // Đóng cửa sổ
            }
        }

        
        private Visibility _loginPageVisibility = Visibility.Visible;
        public Visibility LoginPageVisibility
        {
            get => _loginPageVisibility;
            set
            {
                _loginPageVisibility = value;
                OnPropertyChanged(nameof(LoginPageVisibility));
            }
        }

        private Visibility _dashboardPageVisibility = Visibility.Collapsed;
        public Visibility DashboardPageVisibility
        {
            get => _dashboardPageVisibility;
            set
            {
                _dashboardPageVisibility = value;
                OnPropertyChanged(nameof(DashboardPageVisibility));
            }
        }

       // public ICommand LoginCommand { get; }

        public LoginViewModel(IAccountService accountService, INewsService newsService, 
            ExpertViewModel expert, FarmerModelView farmer, NewsViewModel news, ProfileModelView profile)
        {
            this.accountService = accountService;
            this.newsService = newsService;
            LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecute);
            Accounts = new ObservableCollection<Account>();
            _ExpertViewModel = expert;
            _farmerViewModel = farmer;
            _NewsViewModel = news;
            _profileViewModel= profile;
 
            isCloseCommand = new RelayCommand(ExcuteCloseLogin);
           // CommandManager.InvalidateRequerySuggested();
            _ = LoadAccounts();
        }
       
        private void ExecuteLoginCommand(object obj)
        {
            var isValidUser = checkLogin(Username, Password);

            if (isValidUser)
            {
                ErrorMessage = "Login successful";

                // Update the frame to show DashboardPage if obj is MainWindow
                if (Application.Current.MainWindow is DashboardMU mainWindow)
                {
                    mainWindow.DashboardPage.Navigate(new DashBoardPage(_ExpertViewModel, _NewsViewModel, 
                        _farmerViewModel, _profileViewModel));

                }
            }
            else
            {
                ErrorMessage = "* Invalid username or password";
            }
        }


    }
}
