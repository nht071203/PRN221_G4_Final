using Microsoft.Identity.Client;
using PRN221_BusinessLogic.Interface;
using PRN221_Models.DTO;
using PRN221_Models.Models;
using PRN221_Repository.AccountRepo;
using PRN221_Repository.CommentRepo;
using PRN221_Repository.LikePostRepo;
using PRN221_Repository.PostImageRepo;
using PRN221_Repository.PostsRepo;
using PRN221_Repository.SharePostRepo;
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
        private readonly ILikePostRepository _likePostRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ISharePostRepository _sharePostRepository;

        public PostService(IPostRepository postRepository, IPostImageRepository postImageRepository, IAccountRepository accountRepository, ILikePostRepository likePostRepository, ICommentRepository commentRepository, ISharePostRepository sharePostRepository)
        {
            _postRepository = postRepository;
            _postImageRepository = postImageRepository;
            _accountRepository = accountRepository;
            _likePostRepository = likePostRepository;
            _commentRepository = commentRepository;
            _sharePostRepository = sharePostRepository;
        }

        public async Task<List<PostDTO>> GetAllPostByAccountId(int id)
        {
            var response = new List<PostDTO>();
            var listPost = await _postRepository.GetAllPostByAccountId(id);

            foreach (var item in listPost)
            {
                var listImageByPost = await _postImageRepository.GetAllByPostId(item.PostId);
                var account = await _accountRepository.GetAccountById((int)item.AccountId);
                var likePost = await _likePostRepository.GetAllLikePostByPostId(item.PostId);
                var comment = await _commentRepository.GetAllCommentPostByPostId(item.PostId);
                var sharePost = await _sharePostRepository.GetAllSharePostByPostId(item.PostId);

                var postItemDto = new PostDTO(item, listImageByPost, account, likePost, comment, sharePost);

                response.Add(postItemDto);
            }

            return response;
        }

        public async Task<List<PostImage>> GetAllPostImagesByAccountId(int id)
        {
            var allImages = new List<PostImage>();

            // Lấy tất cả bài viết của tài khoản theo accountId
            var listPost = await _postRepository.GetAllPostByAccountId(id);

            // Lặp qua từng bài viết và lấy hình ảnh
            foreach (var post in listPost)
            {
                var listImagesByPost = await _postImageRepository.GetAllByPostId(post.PostId);

                // Thêm hình ảnh vào danh sách tổng hợp
                allImages.AddRange(listImagesByPost);
            }

            return allImages;
        }

        public async Task<Account?> ExpertWithMostPosts()
        {
            return await _postRepository.ExpertWithMostPosts();
        }

        public async Task<Account?> FarmerWithMostPosts()
        {
            return await _postRepository.FarmerWithMostPosts();
        }

        //Lấy toàn bộ post bao gồm đã bị xóa
        public async Task<List<PostDTO>> GetListPostAndImage()
        {
            var response = new List<PostDTO>();
            var listPost = await _postRepository.GetAll();

            foreach (var item in listPost)
            {
                var listImageByPost = await _postImageRepository.GetAllByPostId(item.PostId);
                var account = await _accountRepository.GetAccountById((int)item.AccountId);
                var likePost = await _likePostRepository.GetAllLikePostByPostId(item.PostId);
                var comment = await _commentRepository.GetAllCommentPostByPostId(item.PostId);
                var sharePost = await _sharePostRepository.GetAllSharePostByPostId(item.PostId);

                var postItemDto = new PostDTO(item, listImageByPost, account, likePost, comment, sharePost);

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
            response.account = await _accountRepository.GetAccountById((int)post.AccountId);
            response.postImages = await _postImageRepository.GetAllByPostId(post.PostId);
            response.likePosts = await _likePostRepository.GetAllLikePostByPostId(post.PostId);
            response.comments = await _commentRepository.GetAllCommentPostByPostId(post.PostId);
            response.sharePosts = await _sharePostRepository.GetAllSharePostByPostId(post.PostId);

            return response;
        }

        public async Task<bool> IsPostLikedByUser(int postId, int accountId) => await _likePostRepository.IsPostLikedByUser(postId, accountId);

        public async Task<bool> LikePost(int postId, int accountId) => await _likePostRepository.LikePost(postId, accountId);

        public async Task<bool> UnlikePost(int postId, int accountId) => await _likePostRepository.UnlikePost(postId, accountId);

        public async Task<Post> AddPost(int categoryId, int accountId, string content)
        {
            DateTime currentUtcDateTime = DateTime.UtcNow;

            var post = new Post
            {
                CategoryPostId = categoryId,
                AccountId = accountId,
                PostContent = content,
                CreatedAt = currentUtcDateTime,
                IsDeleted = false
            };

            return await _postRepository.AddPost(post);

        }

        //Lấy list post không bị xóa
        public async Task<List<PostDTO>> GetListPostAvailable()
        {
            var response = new List<PostDTO>();
            var listPost = await _postRepository.GetAll();

            foreach (var item in listPost)
            {
                var listImageByPost = await _postImageRepository.GetAllByPostId(item.PostId);
                var account = await _accountRepository.GetAccountById((int)item.AccountId);
                var likePost = await _likePostRepository.GetAllLikePostByPostId(item.PostId);
                var comment = await _commentRepository.GetAllCommentPostByPostId(item.PostId);
                var sharePost = await _sharePostRepository.GetAllSharePostByPostId(item.PostId);

                var postItemDto = new PostDTO(item, listImageByPost, account, likePost, comment, sharePost);

                if(item.IsDeleted == false)
                {
                    response.Add(postItemDto);
                }
            }

            return response;
        }
    }
}
