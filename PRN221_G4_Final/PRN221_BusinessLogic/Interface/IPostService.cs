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
        Task<PostDTO> GetPostAndImage(int postId);
        Task<bool> LikePost(int postId, int accountId);
        Task<bool> UnlikePost(int postId, int accountId);
        Task<bool> IsPostLikedByUser(int postId, int accountId);
        Task<List<PostDTO>> GetAllPostByAccountId(int id);
        Task<List<PostDTO>> GetAllPostImagesByAccountId(int id);
    }
}
