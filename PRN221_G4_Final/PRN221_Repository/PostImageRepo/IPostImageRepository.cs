using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.PostImageRepo
{
    public interface IPostImageRepository
    {
        Task<IEnumerable<PostImage>> GetAll();
        Task<IEnumerable<PostImage>> GetAllByPostId(int postId);
        Task AddPostImage(PostImage postImage);
        Task<int> DeleteImage(PostImage postImage);
    }
}
