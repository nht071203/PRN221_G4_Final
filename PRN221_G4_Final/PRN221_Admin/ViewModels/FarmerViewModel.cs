using PRN221_BusinessLogic.Interface;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PRN221_Admin.ViewModels
{
    public class FarmerViewModel : BaseViewModel
    {

        private ObservableCollection<Account> _expert;

        private Account _selectedExpert;

        private readonly IAccountService expertService;
        //public ObservableCollection<Account> Accounts { get; set; } = new ObservableCollection<Account>();

        public ObservableCollection<Account> Accounts
        {
            get { return _expert; }
            set
            {
                _expert = value;
                OnPropertyChanged(nameof(Accounts));

            }
        }




        public FarmerViewModel(IAccountService accountService)
        {
            Accounts = new ObservableCollection<Account>();

            this.expertService = accountService;

            _ = LoadAccounts();


        }


        public async Task LoadAccounts()
        {

            /*var students = await expertService.GetByIdAccount(2);
            students.IsDeleted = true;
            await expertService.UpdateAccount(students);*/
            var students = await expertService.GetListAccountByRoleId(1);
            Accounts.Clear();
            foreach (var student in students)
            {
                if (student.IsDeleted == false)
               {

                Accounts.Add(student);
                }

            }

        }

        public Account SelectedExpert
        {
            get { return _selectedExpert; }
            set
            {
                _selectedExpert = value;
                OnPropertyChanged(nameof(SelectedExpert));


            }
        }
    }
}
