using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_BusinessLogic.Interface
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetListAllRole();
        Task<Role> GetByIdRole(int id);
        //Task AddRole(Role item);
        //Task UpdateRole(Role item);
        //Task DeleteRole(int id);
    }
}
