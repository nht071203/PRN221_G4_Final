﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using PRN221_Models.Models;
using System.Data;
using System.Security.Claims;
using System.Security.Policy;

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
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
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

        public IActionResult OnGetExternalLogin(string provider)
        {
            //Redirect to the specified external provider(Facebook)
            var redirectUrl = Url.Page("/Access/Login", pageHandler: "ExternalLoginCallback");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetExternalLoginCallbackAsync()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!authenticateResult.Succeeded)
            {
                // Authentication failed, return to login page
                return RedirectToPage("/Access/Login");
            }

            // Authentication succeeded, retrieve user information
            var claims = authenticateResult.Principal.Claims;
            var name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var fbId = claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var avatar = claims.FirstOrDefault(c => c.Type == "picture")?.Value;

            var account = await _accountService.GetByIdFacebook(fbId);
            if (account == null)
            {

                await _accountService.CreateNewFacebookAccount(fbId, name, email, avatar);
            }

            if (string.IsNullOrEmpty(avatar) && !string.IsNullOrEmpty(fbId))
            {
                avatar = $"https://graph.facebook.com/{fbId}/picture?type=large";
            }

            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, account.Username),
            //    new Claim(ClaimTypes.Email, email)
            //};

            var claims2 = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, name),
                new Claim(ClaimTypes.Email, email)
            };

            var claimsIdentity = new ClaimsIdentity(claims2, "CookiesPRN221");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
            };

            await HttpContext.SignInAsync("CookiesPRN221", claimsPrincipal, authProperties);

            // Add Session login Facebook
            HttpContext.Session.SetString("UserSession", name);

            // Redirect to the Index page after successful login
            return RedirectToPage("/Index");
        }

        public IActionResult OnGetSignInFacebook()
        {
            return OnGetExternalLogin("Facebook");
        }
    }
}
