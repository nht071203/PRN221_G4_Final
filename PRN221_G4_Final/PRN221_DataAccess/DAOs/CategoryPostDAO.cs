using Microsoft.EntityFrameworkCore;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_DataAccess.DAOs
{
    public class CategoryPostDAO : SingletonBase<CategoryPostDAO>
    {
        public async Task<IEnumerable<CategoryPost>> GetAll()
        {
            return await _context.CategoryPosts.ToListAsync();
        }
    }
}
