using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_BusinessLogic.Interface
{
    public interface IFollowService
    {
        Task<List<Account>> ListFollowers(int id);
        Task<List<Account>> ListFollowing(int id);
        Task<List<Account>> ListFriends(int id);
        Task<Follow> FindById(int follower_id, int followed_id);
        Task Add(Follow item);
        Task Delete(int follower_id, int followed_id);
        Task<bool> IsFollowing(int follower_id, int followed_id);
    }
}
