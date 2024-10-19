using Microsoft.EntityFrameworkCore;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_DataAccess.DAOs
{
    public class FollowDAO : SingletonBase<FollowDAO>
    {
        public async Task<List<Follow>> GetFollowersByAccountId(int id)
        {
            return await _context.Follows
                .Where(f => f.BeFollowedId == id)
                .ToListAsync();
        }

        public async Task<List<Follow>> GetFollowingByAccountId(int id)
        {
            return await _context.Follows
                .Where(f => f.FollowerId == id)
                .ToListAsync();
        }

        public async Task<List<Follow>> GetFriendsByAccountId(int id)
        {
            // Giả sử bạn muốn lấy những người bạn đang theo dõi và cũng đang theo dõi lại
            var following = await GetFollowingByAccountId(id);
            var followers = await GetFollowersByAccountId(id);

            var friendIds = following.Select(f => f.BeFollowedId)
                .Intersect(followers.Select(f => f.FollowerId));

            return await _context.Follows
                .Where(f => friendIds.Contains(f.BeFollowedId))
                .ToListAsync();
        }
    }
}
