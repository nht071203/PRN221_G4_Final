using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using PRN221_BusinessLogic.Service;
using System.Security.Claims;
using PRN221_Models.DTO;
using PRN221_Models.Models;
using PRN221_Client.ViewModel;

namespace PRN221_Client.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPostService _postService;
        private readonly ICategoryPostService _categoryPostService;
        private readonly IPostImageService _postImageService;
        private readonly IImageService _imageService;
        public string? Username { get; set; }
        public string? Role { get; set; }
        public string? FullName { get; set; }
        public List<PostDTO> Posts { get; set; }
        public IEnumerable<CategoryPost> ListCategoryPost { get; set; }
        public Post Post { get; set; }
        private string ImageUrl { get; set; }
        

        public IndexModel(IHttpContextAccessor httpContextAccessor, ILogger<IndexModel> logger, IAccountService accountService, IPostService postService, ICategoryPostService categoryPostService, IPostImageService postImageService, IImageService imageService)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _accountService = accountService;
            _postService = postService;
            _categoryPostService = categoryPostService;
            _postImageService = postImageService;
            _imageService = imageService;
        }

        public async Task<IActionResult> OnGet()
        {
            // Kiểm tra session
            Username = HttpContext.Session.GetString("UserSession");
            Role = HttpContext.Session.GetString("UserRole");

            if(Username != null)
            {
                FullName = await _accountService.GetFullNameByUsername(Username);
                ListCategoryPost = await _categoryPostService.GetAllCategory();
            }

            //LAY LIST POST KEM IMAGE
            Posts = await _postService.GetListPostAvailable();
            
            return Page();
        }

        [BindProperty]
        public string ContentPost { get; set; }

        [BindProperty]
        public int CategoryPostId { get; set; }
        [BindProperty]
        public PostViewModel PostViewModel { get; set; }

        public async Task<IActionResult> OnPostAddPost()
        {
            var account_id = HttpContext.Session.GetInt32("AccountID");

            if(account_id == null)
            {
                return RedirectToPage("/Access/Login");
            }
            Post = await _postService.AddPost(CategoryPostId, (int) account_id, ContentPost);
            if (Post == null)
            {
                ModelState.AddModelError(string.Empty, "Đăng bài không thành công!");
                return RedirectToPage("/Index");
            }

            if (PostViewModel.ImagesPost != null && PostViewModel.ImagesPost.Count > 0)
            {
                foreach (var image in PostViewModel.ImagesPost)
                {
                    ImageUrl = await _imageService.UploadImageAsync(image);
                    await _postImageService.AddPostImage(Post.PostId, ImageUrl);
                } 
            }

            return RedirectToPage("/Index");
        }
    }
}
