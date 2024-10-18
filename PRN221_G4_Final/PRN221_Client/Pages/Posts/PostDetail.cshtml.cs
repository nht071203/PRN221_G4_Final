using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using PRN221_Models.DTO;

namespace PRN221_Client.Pages.Posts
{
    public class PostDetailModel : PageModel
    {
        private readonly IPostService _postService;
        private readonly IViewService _viewService;
        public int PostId { get; set; }
        public PostDTO PostDTO { get; set; }
        public int View {  get; set; }

        public PostDetailModel(IPostService postService, IViewService viewService)
        {
            _postService = postService;
            _viewService = viewService;
        }

        public async Task OnGetAsync(int postId)
        {
            PostId = postId;
            PostDTO = await _postService.GetPostAndImage(PostId);
            View = await _viewService.GetViewByPostId(PostId);
        }
    }
}
