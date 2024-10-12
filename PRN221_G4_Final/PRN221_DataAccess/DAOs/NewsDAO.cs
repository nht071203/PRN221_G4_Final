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
    }
}
