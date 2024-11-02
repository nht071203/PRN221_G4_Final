using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using PRN221_Models.Models;
using System.ComponentModel.DataAnnotations;

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

        [BindProperty]
        [Required(ErrorMessage = "Please enter your password.")]
        public string OldPassword { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter your new password.")]
        [MinLength(6, ErrorMessage = "New password must be at least 6 characters long.")]
        public string NewPassword { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please confirm your new password.")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Profile = await _accountService.GetByIdAccount(id);
            if (Profile == null)
            {
                return NotFound(); 
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page(); 
            }

            Profile = await _accountService.GetByIdAccount(id);
            if (Profile == null)
            {
                return NotFound(); 
            }

            if (Profile.Password != OldPassword)
            {
                TempData["ErrorMessage"] = "Your old password is incorrect!";
                return Page();
            }

            Profile.Password = NewPassword;
            await _accountService.UpdateAccount(Profile);

            TempData["SuccessMessage"] = "Password changed successfully.";
            return RedirectToPage("/Profile/Information", new { id = id });
            //return Page();
        }
    }
}
