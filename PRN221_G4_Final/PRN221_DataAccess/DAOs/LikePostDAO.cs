using Microsoft.EntityFrameworkCore;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_DataAccess.DAOs
{
    public class LikePostDAO : SingletonBase<LikePostDAO>
    {
        public async Task<IEnumerable<LikePost>> GetAllLikePostByPostId(int id)
        {
            return await _context.LikePosts.Where(l => l.PostId == id).ToListAsync();
        }
    }
}
