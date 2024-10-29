using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using PRN221_Models.Models;

namespace PRN221_Client.Pages.Profile
{
    public class ListFollowModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IFollowService _followService;
        public ListFollowModel(IAccountService accountService, IFollowService followService)
        {
            _accountService = accountService;
            _followService = followService;
        }
        public Account Profile { get; set; }
        public List<Account> Followers { get; set; }
        public List<Account> Following { get; set; }
        public List<Account> Friends { get; set; }
        public string ActiveTab { get; set; }
        public int AccountLogin { get; set; }
        public async Task<IActionResult> OnGetAsync(int id, string tab)
        {
            Profile = await _accountService.GetByIdAccount(id);
            AccountLogin = GetLoggedInUserId();
            if (Profile == null)
            {
                return NotFound(); // Handle case when account is not found
            }

            Followers = await _followService.ListFollowers(id);
            Following = await _followService.ListFollowing(id);
            Friends = await _followService.ListFriends(id);
            ActiveTab = tab;
            return Page();
        }
        private int GetLoggedInUserId()
        {
            return HttpContext.Session.GetInt32("AccountID") ?? -1;
        }
    }
}
