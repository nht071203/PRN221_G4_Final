using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_BusinessLogic.Interface
{
    public interface IPostImageService
    {
        Task<IEnumerable<PostImage>> GetPostImagesByPostId(int postId);
        Task AddPostImage(int postId, string urlImage);
        Task<int> DeletePostImage(PostImage postImage);
    }
}
