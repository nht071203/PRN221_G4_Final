using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using System.Security.Claims;

namespace PRN221_Client.Pages.Access
{
    public class LoginModel : PageModel
    {
        private readonly IAuthenService _authenService;

        public LoginModel(IAuthenService authenticationService)
        {
            _authenService = authenticationService;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostLogin()
        {
            var account = await _authenService.Login(Username, Password);

            if (account == null)
            {
                ModelState.AddModelError(string.Empty, "Tài khoản không tồn tại!");
                return Page();
            }

            var role = await _authenService.GetRoleById(account.RoleId);
            if (role == null) return Page();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.Role, role.RoleName)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "CookiesPRN221");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
            };

            await HttpContext.SignInAsync("CookiesPRN221", new ClaimsPrincipal(claimsIdentity), authProperties);
            HttpContext.Session.SetString("UserSession", Username);

            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnGetLogout()
        {
            HttpContext.Session.Remove("UserSession");

            await HttpContext.SignOutAsync("CookiesPRN221");

            return RedirectToPage("/Index");
        }
    }
}
