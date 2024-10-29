using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using PRN221_BusinessLogic.Service;
using PRN221_Models.Models;
using System.ComponentModel.DataAnnotations;

namespace PRN221_Client.Pages.Access
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly IAuthenService _authenService;
        private readonly IEmailSender _emailSender;
        private readonly IAccountService _accountService;

        public ForgotPasswordModel(IAuthenService authenService, IEmailSender emailSender, IAccountService accountService) // Đổi sang IEmailSender
        {
            _authenService = authenService;
            _emailSender = emailSender;
            _accountService = accountService;
        }

        [BindProperty]
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Sai định dạng email")]
        public string ResetEmail { get; set; }

        public async Task OnGet()
        {
        }

        public async Task<IActionResult> OnPostForgotPassword()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var account = await _authenService.GetAccountByEmailForReset(ResetEmail);
            if (account == null)
            {
                ModelState.AddModelError("EmailNotFound", "Email không tồn tại");
                return Page();
            }

            string repEmail = account.Email;

            // Tạo mã OTP gồm 6 chữ số ngẫu nhiên
            var random = new Random();
            int otp = random.Next(100000, 999999); // Tạo số ngẫu nhiên từ 100000 đến 999999 (6 chữ số)

            // Nội dung email với mã OTP
            string emailBody = $"Đây là mã OTP của bạn: {otp}";

            account.Otp = otp;

            await _accountService.UpdateAccount(account);

            // Sử dụng IEmailSender để gửi email
            _emailSender.SendEmail(repEmail, "Mã OTP mật khẩu", emailBody);

            HttpContext.Session.SetInt32("AccountID", account.AccountId);

            return RedirectToPage("/Access/ConfirmForget");
        }
    }

}
