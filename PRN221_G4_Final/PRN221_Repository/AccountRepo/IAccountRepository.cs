using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.AccountRepo
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAll();
        Task<Account?> GetByUsername(string username);

        Task<IEnumerable<Account>> GetListAccByRoleId(int id);
        Task<Account> GetById(int id);
        Task Add(Account item);
        Task Update(Account item);
        Task Delete(int id);
    }
}
