using Microsoft.EntityFrameworkCore;
using PRN221_Models.DTO;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_BusinessLogic.Interface
{
    public interface IPostService
    {
        Task<List<PostDTO>> GetListPostAndImage();
        Task<List<PostDTO>> GetListPostAvailable();
        Task<PostDTO> GetPostAndImage(int postId);
        Task<Account?> FarmerWithMostPosts();
        Task<Account?> ExpertWithMostPosts();
        Task<bool> LikePost(int postId, int accountId);
        Task<bool> UnlikePost(int postId, int accountId);
        Task<bool> IsPostLikedByUser(int postId, int accountId);
        Task<int> GetLikeCountByPostId(int postId);
        Task<List<PostDTO>> GetAllPostByAccountId(int id);
        //Task<List<PostDTO>> GetAllPostImagesByAccountId(int id);
        Task<Post> AddPost(int categoryId, int accountId, string content);
        Task<List<PostImage>> GetAllPostImagesByAccountId(int id);
    }
}
