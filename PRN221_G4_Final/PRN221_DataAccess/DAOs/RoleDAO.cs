using Microsoft.EntityFrameworkCore;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_DataAccess.DAOs
{
    public class RoleDAO : SingletonBase<RoleDAO>
    {
        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> FindById(int id)
        {
            var item = await _context.Roles.FirstOrDefaultAsync(obj => obj.RoleId == id);
            if (item == null) return null;
            return item;
        }

    }
}
