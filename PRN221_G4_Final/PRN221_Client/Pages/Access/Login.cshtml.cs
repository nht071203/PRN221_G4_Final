using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using PRN221_Models.Models;
using System.Data;
using System.Security.Claims;

namespace PRN221_Client.Pages.Access
{
    public class LoginModel : PageModel
    {
        private readonly IAuthenService _authenService;
        private readonly IAccountService _accountService;

        public LoginModel(IAuthenService authenticationService, IAccountService accountService)
        {
            _authenService = authenticationService;
            _accountService = accountService;
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

        public IActionResult OnPostLoginWithGoogle()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Page("/Access/Login/OnGetGoogleResponse")
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }


        public async Task<IActionResult> OnGetGoogleResponse()
        {
            Console.WriteLine("OnGetGoogleResponse called.");
            var authenticateResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            if (!authenticateResult.Succeeded || authenticateResult.Principal == null)
            {
                Console.WriteLine("Authentication failed.");
                return RedirectToPage("/Access/Login");
            }

            var email = authenticateResult.Principal.FindFirstValue(ClaimTypes.Email);
            var fullName = authenticateResult.Principal.FindFirstValue(ClaimTypes.Name);

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(fullName))
            {
                Console.WriteLine("Email or full name is null or empty.");
                return RedirectToPage("/Access/Login");
            }

            var account = await _accountService.GetAccountByEmail(email);
            if (account == null)
            {
                Console.WriteLine("Account not found for email: " + email);
                return RedirectToPage("/Access/OptionsRole");
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, account.Username),
        new Claim(ClaimTypes.Email, email)
    };

            var claimsIdentity = new ClaimsIdentity(claims, "CookiesPRN221");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
            };

            await HttpContext.SignInAsync("CookiesPRN221", claimsPrincipal, authProperties);

            // Store the session for the Google user
            HttpContext.Session.SetString("UserSession", account.Username);
            HttpContext.Session.SetString("UserEmail", email); // Store other info as needed

            Console.WriteLine("User signed in successfully with Google.");

            return RedirectToPage("/Index");
        }
    }
}
