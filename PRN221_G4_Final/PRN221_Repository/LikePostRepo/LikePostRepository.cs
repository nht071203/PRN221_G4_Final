using PRN221_DataAccess.DAOs;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.LikePostRepo
{
    public class LikePostRepository : ILikePostRepository
    {
        private readonly LikePostDAO _likePostDAO;

        public LikePostRepository(LikePostDAO likePostDAO)
        {
            _likePostDAO = likePostDAO;
        }

        public async Task<IEnumerable<LikePost>> GetAllLikePostByPostId(int id) => await _likePostDAO.GetAllLikePostByPostId(id);
    }
}
