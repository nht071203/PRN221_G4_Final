using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using PRN221_Models.DTO;
using PRN221_Models.Models;

namespace PRN221_Client.Pages.Profile
{
    public class PersonalPageModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IPostService _postService;
        public PersonalPageModel(IAccountService accountService, IPostService postService)
        {
            _accountService = accountService;
            _postService = postService;
        }

        public Account Profile { get; set; }
        public int AccountLogin { get; set; }
        public List<PostDTO> Posts { get; set; }
        public IEnumerable<PostImage> PostImages { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Posts = new List<PostDTO>();
            Profile = await _accountService.GetByIdAccount(id);
            AccountLogin = GetLoggedInUserId();
            if (Profile == null)
            {
                return NotFound(); // Handle case when account is not found
            }
            
            Posts = await _postService.GetAllPostByAccountId(id);
            return Page();
        }
        private int GetLoggedInUserId()
        {
            return HttpContext.Session.GetInt32("AccountID") ?? -1;
        }
    }
}
