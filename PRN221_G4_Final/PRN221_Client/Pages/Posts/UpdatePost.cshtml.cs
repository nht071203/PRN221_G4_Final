using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using PRN221_Client.ViewModel;
using PRN221_Models.DTO;
using PRN221_Models.Models;

namespace PRN221_Client.Pages.Posts
{
    public class UpdatePostModel : PageModel
    {
        private readonly IPostService _postService;
        private readonly IPostImageService _postImageService;
        private readonly IImageService _imageService;
        private readonly IAccountService _accountService;
        private readonly ICategoryPostService _categoryPostService;

        private string ImageUrl;
        private IEnumerable<PostImage> listOldImage;

        public PostDTO Post { get; set; }
        public Account Profile { get; set; }
        public IEnumerable<CategoryPost> ListCategoryPost { get; set; }
        public int AccountLogin { get; set; }

        public UpdatePostModel(IPostService postService, IPostImageService postImageService, IImageService imageService, IAccountService accountService, ICategoryPostService categoryPostService)
        {
            _postService = postService;
            _postImageService = postImageService;
            _imageService = imageService;
            _accountService = accountService;
            _categoryPostService = categoryPostService;
        }

        public async Task<IActionResult> OnGet(int postid)
        {
            var account_id = HttpContext.Session.GetInt32("AccountID");

            if (account_id == null)
            {
                return RedirectToPage("/Access/Login");
            }
            
            //Lấy các thông tin của post
            Post = await _postService.GetPostAndImage(postid);

            Profile = await _accountService.GetByIdAccount((int)account_id);
            AccountLogin = GetLoggedInUserId();

            if (Profile == null)
            {
                return RedirectToPage("/Access/Login"); // Handle case when account is not found 
            }

            ListCategoryPost = await _categoryPostService.GetAllCategory();

            return Page();
        }

        private int GetLoggedInUserId()
        {
            return HttpContext.Session.GetInt32("AccountID") ?? -1;
        }

        [BindProperty]
        public string ContentPostUpdate { get; set; }
        [BindProperty]
        public int CategoryIdUpdate { get; set; }
        [BindProperty]
        public bool IsDeleteOldImage { get; set; }
        [BindProperty]
        public PostViewModel PostViewModel { get; set; }
        [BindProperty]
        public int PostId { get; set; }

        public async Task<IActionResult> OnPostUpdatePost(int postid)
        {
            postid = PostId;

            var account_id = HttpContext.Session.GetInt32("AccountID");

            if (account_id == null)
            {
                return RedirectToPage("/Access/Login");
            }

            int updateStatus = await _postService.UpdatePost(postid, CategoryIdUpdate, (int) account_id, ContentPostUpdate);

            //Kiểm tra update thành công hay không
            if (updateStatus < 1)
            {
                ModelState.AddModelError(string.Empty, "Chính sửa bài không thành công!");
                return RedirectToPage("/Profile/PersonalPage", new { id = account_id });
            }

            //Kiểm tra có muốn xóa ảnh cũ hay không
            if(IsDeleteOldImage)
            {
                //Xóa ảnh cũ

                listOldImage = await _postImageService.GetPostImagesByPostId(postid);

                if (listOldImage != null)
                {
                    foreach (var image in listOldImage)
                    {
                         await _postImageService.DeletePostImage(image);
                    }
                }
            }

            //Thêm ảnh mới nếu có
            if (PostViewModel.ImagesPost != null && PostViewModel.ImagesPost.Count > 0)
            {
                foreach (var image in PostViewModel.ImagesPost)
                {
                    ImageUrl = await _imageService.UploadImageAsync(image);
                    await _postImageService.AddPostImage(postid, ImageUrl);
                }
            }

            return RedirectToPage("/Profile/PersonalPage", new { id = account_id });
        }
    }
}
