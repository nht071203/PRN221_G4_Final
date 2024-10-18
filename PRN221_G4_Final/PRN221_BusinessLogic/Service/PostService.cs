using PRN221_BusinessLogic.Interface;
using PRN221_Models.DTO;
using PRN221_Repository.AccountRepo;
using PRN221_Repository.PostImageRepo;
using PRN221_Repository.PostsRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_BusinessLogic.Service
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostImageRepository _postImageRepository;
        private readonly IAccountRepository _accountRepository;

        public PostService(IPostRepository postRepository, IPostImageRepository postImageRepository, IAccountRepository accountRepository)
        {
            _postRepository = postRepository;
            _postImageRepository = postImageRepository;
            _accountRepository = accountRepository;
        }

        public async Task<List<PostDTO>> GetListPostAndImage()
        {
            var response = new List<PostDTO>();
            var listPost = await _postRepository.GetAll();

            foreach (var item in listPost)
            {
                var listImageByPost = await _postImageRepository.GetAllByPostId(item.PostId);
                var account = await _accountRepository.GetAccountById(item.AccountId);
                var postItemDto = new PostDTO(item, listImageByPost, account);

                response.Add(postItemDto);
            }

            return response;
        }

        public async Task<PostDTO> GetPostAndImage(int postId)
        {
            var response = new PostDTO();
            var post = await _postRepository.GetById(postId);

            if (post == null) return null;

            response.post = post;
            response.account = await _accountRepository.GetAccountById(post.AccountId);
            response.postImages = await _postImageRepository.GetAllByPostId(post.PostId);

            return response;
        }
    }
}
