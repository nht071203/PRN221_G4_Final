using PRN221_BusinessLogic.Interface;
using PRN221_Models.Models;
using PRN221_Repository.PostImageRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_BusinessLogic.Service
{
    public class PostImageService : IPostImageService
    {
        private readonly IPostImageRepository _postImageRepository;

        public PostImageService(IPostImageRepository postImageRepository)
        {
            _postImageRepository = postImageRepository;
        }

        public async Task AddPostImage(int postId, string urlImage)
        {
            var postImage = new PostImage();
            postImage.PostId = postId;
            postImage.ImageUrl = urlImage;
            postImage.IsDeleted = false;

            await _postImageRepository.AddPostImage(postImage);
        }
    }
}
