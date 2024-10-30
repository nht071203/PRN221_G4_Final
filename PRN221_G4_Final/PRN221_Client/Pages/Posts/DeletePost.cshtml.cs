using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using PRN221_BusinessLogic.Interface;

namespace PRN221_Client.Pages.Posts
{
    public class DeletePostModel : PageModel
    {
        private readonly IPostService _postService;
        public DeletePostModel(IPostService postService)
        {
            _postService = postService;
        }
        public void OnGet(int postid)
        {
        }

        public async Task<IActionResult> OnGetDeletePost(int postid)
        {
            var account_id = HttpContext.Session.GetInt32("AccountID");

            if (account_id == null)
            {
                return RedirectToPage("/Access/Login");
            }

            var deleteStatus = await _postService.DeletePost(postid);

            if(deleteStatus <= 0)
            {
                ModelState.AddModelError(string.Empty, "Xóa bài không thành công!");
                return RedirectToPage("/Profile/PersonalPage", new { id = account_id });
            }

            return RedirectToPage("/Profile/PersonalPage", new { id = account_id });
        }
    }
}
