using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using PRN221_Models.Models;
using System.ComponentModel.DataAnnotations;

namespace PRN221_Client.Pages.Access
{
    public class ResetPasswordModel : PageModel
    {
        private readonly IAccountService _accountService;
        public ResetPasswordModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public Account getAccount {  get; set; }
        public async Task OnGet()
        {
        }
        [BindProperty]
        [Required(ErrorMessage = "Không được để trống")]
        public string NewPassword { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Không được để trống")]
        [Compare(otherProperty: "NewPassword", ErrorMessage = "Không trùng mật khẩu")]
        public string ConfirmPass { get; set; }
        public async Task<IActionResult> OnPostResetPassword()
        {
            int? getAccId = Convert.ToInt32(HttpContext.Session.GetInt32("AccountID"));
            getAccount = await _accountService.GetByIdAccount(getAccId.Value);
            HttpContext.Session.Remove("AccountID");

            Console.WriteLine(getAccount.AccountId);

            getAccount.Password = NewPassword;

            await _accountService.UpdateAccount(getAccount);

            return RedirectToPage("/Access/Login");
        }
    }
}
