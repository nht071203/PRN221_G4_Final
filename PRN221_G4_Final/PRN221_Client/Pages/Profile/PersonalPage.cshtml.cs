using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using PRN221_BusinessLogic.Service;
using PRN221_Client.ViewModel;
using PRN221_Models.DTO;
using PRN221_Models.Models;

namespace PRN221_Client.Pages.Profile
{
    public class PersonalPageModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IPostService _postService;
        private readonly ICategoryPostService _categoryPostService;
        private readonly IPostImageService _postImageService;
        private readonly IImageService _imageService;
        private readonly IFollowService _followService;
        public PersonalPageModel(IAccountService accountService, IPostService postService, ICategoryPostService categoryPostService, IPostImageService postImageService, IImageService imageService, IFollowService followService )
        {
            _accountService = accountService;
            _postService = postService;
            _categoryPostService = categoryPostService;
            _postImageService = postImageService;
            _imageService = imageService;
            _followService = followService;
        }

        public Account Profile { get; set; }
        public int AccountLogin { get; set; }
        public List<PostDTO> Posts { get; set; }
        public IEnumerable<PostImage> PostImages { get; set; }
        public IEnumerable<CategoryPost> ListCategoryPost { get; set; }
        public Post Post { get; set; }
        private string ImageUrl { get; set; }

        //UserSession là user đang đăng nhập
        public Account UserSession { get; set; }
        public Follow Follow { get; set; }
        public bool IsFollowing { get; set; }   
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Kiểm tra session và lấy thông tin người đang đăng nhập
            var usernameSession = HttpContext.Session.GetString("UserSession");
            if(usernameSession == null)
            {
                return RedirectToPage("/Access/Login");
            }

            UserSession = await _accountService.GetByUsername(usernameSession);

            Posts = new List<PostDTO>();
            Profile = await _accountService.GetByIdAccount(id);
            TempData["ProfileId"] = Profile.AccountId;
            AccountLogin = GetLoggedInUserId();
            if (Profile == null)
            {
                return NotFound(); // Handle case when account is not found 
            }

            ListCategoryPost = await _categoryPostService.GetAllCategory();
            Posts = await _postService.GetAllPostByAccountId(id);

            var follower_id = AccountLogin;
            int followed_id = Profile.AccountId;
            IsFollowing = await _followService.IsFollowing(follower_id, followed_id);
            return Page();
        }
        private int GetLoggedInUserId()
        {
            return HttpContext.Session.GetInt32("AccountID") ?? -1;
        }


        //XỬ LÝ ADD POST TRONG PERSONAL PAGE
        [BindProperty]
        public string ContentPost { get; set; }

        [BindProperty]
        public int CategoryPostId { get; set; }
        [BindProperty]
        public PostViewModel PostViewModel { get; set; }
        [BindProperty]
        public int AccountID { get; set; }

        public async Task<IActionResult> OnPostAddPost()
        {
            var account_id = HttpContext.Session.GetInt32("AccountID");

            if (account_id == null)
            {
                return RedirectToPage("/Access/Login");
            }

            Post = await _postService.AddPost(CategoryPostId, (int)account_id, ContentPost);
            if (Post == null)
            {
                ModelState.AddModelError(string.Empty, "Đăng bài không thành công!");
                return RedirectToPage("/Profile/PersonalPage", account_id);
            }

            if (PostViewModel.ImagesPost != null && PostViewModel.ImagesPost.Count > 0)
            {
                foreach (var image in PostViewModel.ImagesPost)
                {
                    ImageUrl = await _imageService.UploadImageAsync(image);
                    await _postImageService.AddPostImage(Post.PostId, ImageUrl);
                }
            }

            return RedirectToPage("/Profile/PersonalPage", new { id = account_id });
        }

        public async Task<IActionResult> OnPostFollow(Follow follow)
        {
            var follower_id = HttpContext.Session.GetInt32("AccountID");
            int followed_id = (int)TempData["ProfileId"];
            if (follower_id != null)
            {
                follow.FollowerId = (int)follower_id;
                follow.BeFollowedId = followed_id;
            }
            Follow = follow;
            await _followService.Add(Follow);
            return RedirectToPage("/Profile/PersonalPage", new { id = followed_id });
        }

        public async Task<IActionResult> OnPostUnfollow(Follow follow)
        {
            var follower_id = HttpContext.Session.GetInt32("AccountID");
            int followed_id = (int)TempData["ProfileId"];
            
            follow = await _followService.FindById((int)follower_id, followed_id);
            Follow = follow;
            await _followService.Delete((int)follower_id, followed_id);
            return RedirectToPage("/Profile/PersonalPage", new { id = followed_id });
        }
    }
}
