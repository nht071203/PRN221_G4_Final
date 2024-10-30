using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using PRN221_Models.Models;

namespace PRN221_Client.Pages.Profile
{
    public class ChangePasswordModel : PageModel
    {
        private readonly IAccountService _accountService;

        public ChangePasswordModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public Account Profile { get; set; }
        //public int AccountLogin { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Profile = await _accountService.GetByIdAccount(id);
            //AccountLogin = GetLoggedInUserId();
            if (Profile == null)
            {
                return NotFound(); // Handle case when account is not found 
            }

            return Page();
        }
    }
}
