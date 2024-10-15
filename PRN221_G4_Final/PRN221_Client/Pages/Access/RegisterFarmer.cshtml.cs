using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using PRN221_Models.Models;

namespace PRN221_Client.Pages.Access
{
    public class RegisterFarmerModel : PageModel
    {
        private readonly IAuthenService _authenService;

        public RegisterFarmerModel(IAuthenService authenService)
        {
            _authenService = authenService;
        }

        [BindProperty]
        public string FullName { get; set; }
        [BindProperty]
        public string Gender { get; set; }
        [BindProperty]
        public string PhoneNumber { get; set; }
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostRegisterFarmer()
        {
            var Account = new Account
            {
                FullName = this.FullName,
                Gender = this.Gender,
                Phone = this.PhoneNumber,
                Username = this.Username,
                Password = this.Password,
                Email = "Unknown",
                RoleId = 1,
                IsDeleted = false
            };

            var newAccount = await _authenService.Register(Account);

            if (newAccount == null) {
                ModelState.AddModelError(string.Empty, "Đăng ký thất bại!");
                return Page();
            }

            return RedirectToPage("/Access/Login");
        }
    }
}
