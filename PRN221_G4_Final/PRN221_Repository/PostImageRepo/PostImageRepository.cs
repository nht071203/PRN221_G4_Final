using PRN221_DataAccess.DAOs;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.PostImageRepo
{
    public class PostImageRepository : IPostImageRepository
    {
        private readonly PostImageDAO _postImageDAO;
        public PostImageRepository(PostImageDAO postImageDAO)
        {
            _postImageDAO = postImageDAO;
        }

        public async Task<IEnumerable<PostImage>> GetAll()
        {
            return await _postImageDAO.GetAll();
        }

        public async Task<IEnumerable<PostImage>> GetAllByPostId(int postId)
        {
            return await _postImageDAO.GetAllByPostId(postId);
        }
    }
}
