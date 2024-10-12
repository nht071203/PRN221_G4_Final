using PRN221_DataAccess.DAOs;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.RoleRepo
{
    public class RoleRepository : IRoleRepository
    {
        private RoleDAO roleDAO;
        public RoleRepository(RoleDAO roleDAO) {
            this.roleDAO = roleDAO;
        }

        public async Task<IEnumerable<Role>> GetListAll()
        {
            return await roleDAO.GetAll();
        }

        public async Task<Role?> getById(int id)
        {
            return await roleDAO.FindById(id);
        }





    }
}
