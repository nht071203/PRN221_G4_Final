using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.FollowRepo
{
    public interface IFollowRepository
    {
        Task<List<Follow>> GetFollowersByAccountId(int id);
        Task<List<Follow>> GetFollowingByAccountId(int id);
        Task<List<Follow>> GetFriendsByAccountId(int id);
        Task<Follow> FindById(int follower_id, int followed_id);
        Task Add(Follow item);
        Task Delete(int follower_id, int followed_id);
    }
}
