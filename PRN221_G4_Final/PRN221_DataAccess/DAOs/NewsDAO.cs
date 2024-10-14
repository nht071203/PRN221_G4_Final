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
        private readonly Prn221Context _context; 

        public NewsDAO()
        {
            _context = new Prn221Context(); 
        }

        public NewsDAO(Prn221Context context)
        {
            _context = context;
        }

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

        public async Task<CategoryNews> GetCategoryNewsById(int id)
        {
            var item = await _context.CategoryNews.FirstOrDefaultAsync(obj => obj.CategoryNewsId == id);
            if (item == null) return null;
            return item;
        }

        public async Task<IEnumerable<CategoryNews>> GetAllCategoryNews()
        {
            return await _context.CategoryNews.ToListAsync();
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
            item.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);
            _context.Entry(existingItem).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var item = await FindById(id);
            if (item == null) return;

            item.IsDeleted = true;
            item.DeletedAt = DateOnly.FromDateTime(DateTime.Now);
            _context.News.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<(string Month, int Count)>> GetNewsCountByMonth()
        {
            var result = await _context.News
                .Where(n => n.IsDeleted != true) // Lọc các bài viết không bị xóa
                .GroupBy(n => new { n.CreatedAt.Year, n.CreatedAt.Month })
                .Select(g => new
                {
                    Month = $"{g.Key.Year}-{g.Key.Month:D2}", // Định dạng năm-tháng
                    Count = g.Count()
                })
                .ToListAsync();

            // Chuyển đổi kết quả sang tuple
            return result.Select(item => (item.Month, item.Count));
        }

        

    }
}

