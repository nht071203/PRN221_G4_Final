using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using PRN221_BusinessLogic.Interface;
using PRN221_Models.Models;
using System.ComponentModel.DataAnnotations;

namespace PRN221_Client.Pages.Access
{
    public class ConfirmForgetModel : PageModel
    {
        private readonly IAccountService _accountService;
        public ConfirmForgetModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public Account getAccount { get; set; }
        public async Task OnGet()
        {
        }

        [BindProperty]
        [Required]
        public int OTP { get; set; }
        public async Task<IActionResult> OnPostConfirmOTP()
        {
            int? getAccId = Convert.ToInt32(HttpContext.Session.GetInt32("AccountID"));
            getAccount = await _accountService.GetByIdAccount(getAccId.Value);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            int? checkOTP = getAccount.Otp;

            if (OTP != getAccount.Otp) {
                ModelState.AddModelError(string.Empty, "OTP không khớp");
                return Page();
            }

            return RedirectToPage("/Access/ResetPassword");
        }
    }
}
