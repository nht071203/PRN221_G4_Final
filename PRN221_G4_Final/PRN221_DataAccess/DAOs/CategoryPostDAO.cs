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
        public async Task<CategoryPost> FindById(int id)
        {
            var item = await _context.CategoryPosts.FirstOrDefaultAsync(obj => obj.CategoryPostId == id);
            if (item == null) return null;
            return item;
        }
        public async Task Add(CategoryPost item)
        {
            _context.CategoryPosts.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(CategoryPost item)
        {
            var existingItem = await FindById(item.CategoryPostId);
            if (existingItem == null) return;
            _context.Entry(existingItem).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var item = await FindById(id);
            if (item == null) return;
          _context.CategoryPosts.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
