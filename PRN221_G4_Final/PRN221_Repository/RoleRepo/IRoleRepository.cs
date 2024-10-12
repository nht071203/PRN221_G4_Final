using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.RoleRepo
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetListAll();
        Task<Role?> getById(int id);
    }
}
