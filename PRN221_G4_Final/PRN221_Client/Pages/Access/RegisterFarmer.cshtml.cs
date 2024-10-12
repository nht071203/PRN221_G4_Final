using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;

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
    }
}
