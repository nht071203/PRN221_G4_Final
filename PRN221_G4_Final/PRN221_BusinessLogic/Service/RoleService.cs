using PRN221_BusinessLogic.Interface;
using PRN221_Models.Models;
using PRN221_Repository.RoleRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_BusinessLogic.Service
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepo;
        public RoleService(IRoleRepository roleRepo)
        {
            _roleRepo = roleRepo;
        }
        public async Task<IEnumerable<Role>> GetListAllRole()
        {
            return await _roleRepo.GetListAll();
        }
        public async Task<Role> GetByIdRole(int id)
        {
            return await _roleRepo.getById(id);
        }
        //public async Task AddRole(Role item)
        //{
        //    await _roleRepo.Add(item);
        //}
        //public async Task UpdateRole(Role item)
        //{
        //    await _roleRepo.Update(item);
        //}
        //public async Task DeleteRole(int id)
        //{
        //    await _roleRepo.Delete(id);
        //}

    }

}
