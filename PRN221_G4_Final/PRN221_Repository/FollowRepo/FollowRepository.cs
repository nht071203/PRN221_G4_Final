using PRN221_DataAccess.DAOs;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.FollowRepo
{
    public class FollowRepository : IFollowRepository
    {
        private readonly FollowDAO _followDAO;

        public FollowRepository(FollowDAO followDAO)
        {
            _followDAO = followDAO;
        }

        public async Task<List<Follow>> GetFollowersByAccountId(int id) => await _followDAO.GetFollowersByAccountId(id);

        public async Task<List<Follow>> GetFollowingByAccountId(int id) => await _followDAO.GetFollowingByAccountId(id);

        public async Task<List<Follow>> GetFriendsByAccountId(int id) => await _followDAO.GetFriendsByAccountId(id);
    }
}
