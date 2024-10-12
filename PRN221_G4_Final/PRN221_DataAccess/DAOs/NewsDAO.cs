using Microsoft.EntityFrameworkCore;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_DataAccess.DAOs
{
    public class NewsDAO : SingletonBase<NewsDAO>
    {
        public async Task<IEnumerable<News>> GetAllNews()
        {
            return await _context.News.ToListAsync();
        }
        public async Task<IEnumerable<News>> GetAll()
        {
            return await _context.News.ToListAsync();
        }

        public async Task<News> FindById(int id)
        {
            var item = await _context.News.FirstOrDefaultAsync(obj => obj.NewsId == id);
            if (item == null) return null;
            return item;
        }
        public async Task Add(News item)
        {
            _context.News.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(News item)
        {
            var existingItem = await FindById(item.NewsId);
            if (existingItem == null) return;

            _context.Entry(existingItem).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var item = await FindById(id);
            if (item == null) return;

            _context.News.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}

