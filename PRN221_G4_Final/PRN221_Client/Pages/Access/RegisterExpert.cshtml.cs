using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PRN221_BusinessLogic.Interface;
using PRN221_Models.Models;
using System.ComponentModel.DataAnnotations;

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
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
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
        [BindProperty]
        public int YearOfExperience { get; set; }
        [BindProperty]
        public string ShortBio {  get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostRegisterExpert()
        {
            var fileDegree = Request.Form.Files["fileDegree"];
            var fileAvatar = Request.Form.Files["fileAvatar"];
            var fileEducation = Request.Form.Files["fileEducation"];


            string urlDegree = await _imageService.UploadImageAsync(fileDegree);
            string urlAvatar = await _imageService.UploadImageAsync(fileAvatar);
            string urlEducation = await _imageService.UploadImageAsync(fileEducation);

            var account = new Account
            {
                Username = this.Username,
                Password = this.Password,
                FullName = this.FullName,
                DateOfBirth = this.DateOfBirth,
                Email = this.Email,
                Phone = this.PhoneNumber,
                Gender = this.Gender,
                Address = this.Address,
                Major = this.Major,
                YearOfExperience = this.YearOfExperience,
                ShortBio = this.ShortBio,
                DegreeUrl = urlDegree,
                Avatar = urlAvatar,
                EducationUrl = urlEducation,
                IsDeleted = false,
                RoleId = 2
            };


            TempData["AccountRegister"] = JsonConvert.SerializeObject(account);

            return RedirectToPage("/Access/ConfirmEmail");
        }
    }
}
