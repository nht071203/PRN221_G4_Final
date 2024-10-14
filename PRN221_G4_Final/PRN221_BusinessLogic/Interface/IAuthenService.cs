using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_BusinessLogic.Interface
{
    public interface IAuthenService
    {
        Task<Account?> Login(string username, string password);
        Task<Role?> GetRoleById(int roleId);
        Task<Account> Register(Account account);
    }
}
