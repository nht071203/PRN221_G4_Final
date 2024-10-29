using Microsoft.EntityFrameworkCore;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_DataAccess.DAOs
{
    public class SharePostDAO : SingletonBase<SharePostDAO> 
    {
        public async Task<IEnumerable<SharePost>> GetAllSharePostByPostId(int id)
        {
            return await _context.SharePosts.Where(l => l.PostId == id).ToListAsync();
        }
    }
}
