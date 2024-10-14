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

        public FarmerModelView(IAccountService accountService)
        {
            Farmer = new ObservableCollection<Account>();
            this.accountService = accountService;
            _ = LoadFarmers();
        }
        private Account _selectedFarmer;
        public Account SelectedFarmer
        {
            get => _selectedFarmer;
            set
            {
                _selectedFarmer = value;
                OnPropertyChanged(nameof(SelectedFarmer));

            }
        }


    }
}
