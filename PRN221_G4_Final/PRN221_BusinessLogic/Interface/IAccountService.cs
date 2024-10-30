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

        Task<Account> GetByUsername(string username);

        Task<Account> GetByIdFacebook(string fbId);

		Task AddAccount(Account item);

        Task CreateNewFacebookAccount(string fbId, string name, string email, string avatar);
        Task UpdateAccount(Account item);
        Task DeleteAccount(Account item);
        Task<Account?> GetAccountByEmail(string email);
        Task<int> GetTotalFarmerService();
        Task<string?> GetFullNameByUsername(string username);
        Task<int> GetTotalExpertService();
        Task<bool> ChangePasswordAsync(int id, string oldPassword, string newPassword);
    }
}
