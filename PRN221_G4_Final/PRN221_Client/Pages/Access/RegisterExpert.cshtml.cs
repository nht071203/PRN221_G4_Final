using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using PRN221_Models.Models;

namespace PRN221_Client.Pages.Access
{
    public class RegisterExpertModel : PageModel
    {
        private readonly IAuthenService _authenService;
        private readonly IImageService _imageService;
        public RegisterExpertModel(IAuthenService authenService, IImageService imageService)
        {
            _authenService = authenService;
            _imageService = imageService;
        }

        [BindProperty]
        public string FullName { get; set; }
        [BindProperty]
        public string Gender { get; set; }
        [BindProperty]
        public string Address { get; set; }
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string Repassword { get; set; }
        [BindProperty]
        public string PhoneNumber { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Major {  get; set; }
 
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostRegisterExpert()
        {
            var fileDegree = Request.Form.Files["fileDegree"];
            var fileAvatar = Request.Form.Files["fileAvatar"];


            string urlDegree = await _imageService.UploadImageAsync(fileDegree);
            string urlAvatar = await _imageService.UploadImageAsync(fileAvatar);

            var account = new Account
            {
                Username = this.Username,
                Password = this.Password,
                FullName = this.FullName,
                Email = this.Email,
                Phone = this.PhoneNumber,
                Gender = this.Gender,
                Address = this.Address,
                Major = this.Major,
                DegreeUrl = urlDegree,
                Avatar = urlAvatar,
                IsDeleted = false,
                RoleId = 2
            };

            var newExpert = _authenService.Register(account);

            if (newExpert == null )
            {
                ModelState.AddModelError(string.Empty, "Đăng ký thất bại!");
                return Page();
            }

            return RedirectToPage("/Access/Login");
        }
    }
}
