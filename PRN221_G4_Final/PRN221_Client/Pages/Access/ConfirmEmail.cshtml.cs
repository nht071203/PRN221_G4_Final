using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PRN221_BusinessLogic.Interface;
using PRN221_Models.Models;
using static System.Net.WebRequestMethods;

namespace PRN221_Client.Pages.Access
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly PRN221_BusinessLogic.Interface.IEmailSender _emailSender;
        private readonly IAuthenService _authenService;
        private readonly IImageService _imageService;
        public ConfirmEmailModel(PRN221_BusinessLogic.Interface.IEmailSender emailSender, IAuthenService authenService, IImageService imageService)
        {
            _emailSender = emailSender;
            _authenService = authenService;
            _imageService = imageService;
        }

        [BindProperty]
        public int OTPEmail {  get; set; }

        public IActionResult OnGet()
        {
            var accountJson = TempData["AccountRegister"] as string;
            TempData.Keep("AccountRegister");
            var account = JsonConvert.DeserializeObject<Account>(accountJson);

            if (account == null) {
                return RedirectToPage("/Access/RegisterExpert");
            }

            //Tạo OTP ngẫu nhiên
            int otp = new Random().Next(100000, 999999);

            //Lưu OTP vào session và set time lúc lưu
            HttpContext.Session.SetInt32("OTPStore", otp);
            HttpContext.Session.SetString("OTPCreatedTime", DateTime.Now.ToString());
            
            //Gửi mail
            _emailSender.SendEmail(account.Email, "Mã OTP xác nhận đăng ký", "Mã xác nhận là: " + otp);

            return Page();
        }

        public async Task<IActionResult> OnPostConfirmOTP()
        {
            var OTPStored = HttpContext.Session.GetInt32("OTPStore");
            var OTPCreatedTimeStr = HttpContext.Session.GetString("OTPCreatedTime");

            if(OTPStored == null || OTPCreatedTimeStr == null)
            {
                ModelState.AddModelError(string.Empty, "OTP không hợp lệ.");
                return RedirectToPage("/Access/Register");
            }

            // Chuyển đổi thời gian tạo OTP từ string thành DateTime
            DateTime otpCreatedTime = DateTime.Parse(OTPCreatedTimeStr);

            // Kiểm tra OTP đã hết hạn hay chưa
            if ((DateTime.Now - otpCreatedTime).TotalMinutes > 3)
            {
                ModelState.AddModelError(string.Empty, "OTP đã hết hạn.");
                return Page();
            }

            // So sánh OTP người dùng nhập vào với OTP đã lưu
            if (OTPEmail != OTPStored)
            {
                ModelState.AddModelError(string.Empty, "OTP không chính xác.");
                return Page();
            }

            var accountJsonConfirm = TempData["AccountRegister"] as string;
            var accountConfirm = JsonConvert.DeserializeObject<Account>(accountJsonConfirm);

            var newExpert = await _authenService.Register(accountConfirm);

            if (newExpert == null)
            {
                await _imageService.DeleteImageAsync(accountConfirm.Avatar);
                await _imageService.DeleteImageAsync(accountConfirm.DegreeUrl);
                await _imageService.DeleteImageAsync(accountConfirm.EducationUrl);
                ModelState.AddModelError(string.Empty, "Đăng ký thất bại!");
                return RedirectToPage("/Access/RegisterExpert");
            }

            return RedirectToPage("/Access/Login");
        }
    }
}
