using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_BusinessLogic.Interface
{
    public interface IAccountService

    {
        Task<IEnumerable<Account>> GetListAllAccount();
        Task<IEnumerable<Account>> GetListAccountByRoleId(int role_id);
        Task<Account> GetByIdAccount(int id);
        Task<Account> GetByIdFacebook(string fbId);
		Task<Account> GetByUsername(string username);
		Task AddAccount(Account item);
        Task CreateNewFacebookAccount(string fbId, string name, string email, string avatar);
        Task UpdateAccount(Account item);
        Task DeleteAccount(int id);
        Task<Account?> GetAccountByEmail(string email);
    }
}
