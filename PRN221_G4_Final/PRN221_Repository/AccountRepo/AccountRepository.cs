using PRN221_DataAccess.DAOs;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.AccountRepo
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountDAO _accountDAO;

        public AccountRepository(AccountDAO accountDAO)
        {
            _accountDAO = accountDAO;
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            return await _accountDAO.getAll();
        }

        public async Task<Account?> GetByUsername(string username)
        {
            return await _accountDAO.getByUsername(username);
        }



        public async Task<IEnumerable<Account>> GetListAccByRoleId(int role_id) => await _accountDAO.GetListAccountByRoleId(role_id);
        public async Task<Account> GetById(int id) => await _accountDAO.GetById(id);
        public async Task<Account> Add(Account account) => await _accountDAO.Add(account);
        public async Task Update(Account account) => await _accountDAO.Update(account);
        public async Task Delete(int id) => await _accountDAO.Delete(id);

        public async Task<Account?> GetAccountByEmail(string email)
        {
            return await _accountDAO.GetAccountByEmail(email);
        }
    }
}
