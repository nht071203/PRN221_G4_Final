using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using PRN221_Models.Models;

namespace PRN221_Client.Pages.Profile
{
    public class PersonalPageModel : PageModel
    {
        private readonly IAccountService _accountService;

        public PersonalPageModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public Account Profile { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Profile = await _accountService.GetByIdAccount(id);
            if (Profile == null)
            {
                return NotFound(); // Handle case when account is not found
            }
            return Page();
        }
    }
}
