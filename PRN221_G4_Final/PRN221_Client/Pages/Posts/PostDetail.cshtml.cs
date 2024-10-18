using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using PRN221_BusinessLogic.Service;
using PRN221_Models.DTO;
using PRN221_Models.Models;

namespace PRN221_Client.Pages.Posts
{
    public class PostDetailModel : PageModel
    {
        private readonly IPostService _postService;
        private readonly IViewService _viewService;
        private readonly IAccountService _accountService;
        public int PostId { get; set; }
        public PostDTO PostDTO { get; set; }
        public int CountLikePost { get; set; }
        public int CountSharePost { get; set; }
        public int CountCommentPost { get; set; }
        public int View {  get; set; }
        public IEnumerable<Comment> ListComment { get; set; }
        public Dictionary<int, Account> CommentAccounts { get; set; } = new Dictionary<int, Account>();

        public PostDetailModel(IPostService postService, IViewService viewService, IAccountService accountService)
        {
            _postService = postService;
            _viewService = viewService;
            _accountService = accountService;
        }

        public async Task OnGetAsync(int postId)
        {
            PostId = postId;
            PostDTO = await _postService.GetPostAndImage(PostId);
            CountLikePost = PostDTO.likePosts.Count();
            CountSharePost = PostDTO.sharePosts.Count();
            CountCommentPost = PostDTO.comments.Count();
            ListComment = PostDTO.comments.ToList();
            // Lấy thông tin tài khoản cho từng bình luận
            foreach (var comment in ListComment)
            {
                if (comment.AccountId.HasValue)
                {
                    var account = await _accountService.GetByIdAccount(comment.AccountId.Value);
                    CommentAccounts[comment.AccountId.Value] = account;
                }
            }
            View = await _viewService.GetViewByPostId(PostId);
        }
    }
}
