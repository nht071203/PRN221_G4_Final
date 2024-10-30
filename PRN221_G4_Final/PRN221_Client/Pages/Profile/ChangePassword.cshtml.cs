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

        // Properties for password change
        [Required]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The new password must be at least {2} characters long.", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "The confirmation password does not match.")]
        public string ConfirmNewPassword { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Profile = await _accountService.GetByIdAccount(id);
            if (Profile == null)
            {
                return NotFound(); // Handle case when account is not found 
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Return page with validation errors
            }

            // Call the service to change the password
            var result = await _accountService.ChangePasswordAsync(id, OldPassword, NewPassword);
            if (result)
            {
                // Optionally add a success message
                TempData["SuccessMessage"] = "Password changed successfully.";
                return RedirectToPage("/Profile/PersonalPage", new { id = id });
            }

            // Add an error message if the change failed
            ModelState.AddModelError(string.Empty, "Failed to change password. Please check your old password and try again.");
            return Page();
        }
    }
}
