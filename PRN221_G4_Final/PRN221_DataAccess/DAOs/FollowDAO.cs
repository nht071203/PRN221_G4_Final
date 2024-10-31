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
        public async Task<Follow> FindById(int follower_id, int followed_id)
        {
            var item = await _context.Follows.FirstOrDefaultAsync(f => f.FollowerId == follower_id && f.BeFollowedId == followed_id);
            if (item == null) return null;
            return item;
        }
        public async Task Add(Follow item)
        {
            item.FollowAt = DateTime.Now;
            _context.Follows.Add(item);
            await _context.SaveChangesAsync();
        }

        //public async Task Update(Follow item)
        //{
        //    var existingItem = await FindById(item.FollowId);
        //    if (existingItem == null) return;
        //    _context.Entry(existingItem).CurrentValues.SetValues(item);
        //    await _context.SaveChangesAsync();
        //}

        public async Task Delete(int follower_id, int followed_id)
        {
            var item = await FindById(follower_id, followed_id);
            if (item == null) return;

            _context.Follows.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
