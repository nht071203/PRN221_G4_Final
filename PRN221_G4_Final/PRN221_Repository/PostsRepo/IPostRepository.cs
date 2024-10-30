using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.PostsRepo
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAll();
        Task<Post?> GetById(int postId);
        Task<Post> AddPost(Post post);
        Task<int> DeletePost(int postId);
        Task<Account> FarmerWithMostPosts();

        Task<Account> ExpertWithMostPosts();

        Task<IEnumerable<Post>> GetAllPostByAccountId(int id);

    }
}
