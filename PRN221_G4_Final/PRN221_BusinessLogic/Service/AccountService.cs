using PRN221_BusinessLogic.Interface;
using PRN221_Models.Models;
using PRN221_Repository.AccountRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_BusinessLogic.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepo;
        public AccountService(IAccountRepository accountRepo)
        {
            _accountRepo = accountRepo;
        }
        public async Task<IEnumerable<Account>> GetListAllAccount()
        {
            return await _accountRepo.GetAll();
        }

        public async Task<IEnumerable<Account>> GetListAccountByRoleId(int id)
        {
            return await _accountRepo.GetListAccByRoleId(id);
        }
        public async Task<Account> GetByIdAccount(int id)
        {
            return await _accountRepo.GetById(id);
        }

        public async Task<Account> GetByUsername(string username)
        {
            return await _accountRepo.GetByUsername(username);
        }
        public async Task AddAccount(Account item)
        {
            await _accountRepo.Add(item);
        }
        public async Task UpdateAccount(Account item)
        {
            await _accountRepo.Update(item);
        }
        public async Task DeleteAccount(Account item)
        {
            await _accountRepo.Delete(item);
        }

        public async Task<Account?> GetAccountByEmail(string email)
        {
            return await _accountRepo.GetAccountByEmail(email);
        }

        public async Task<int> GetTotalFarmerService()
        {
            return await _accountRepo.GetTotalFarmerRepo();
        }
    }
}