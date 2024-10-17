using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using PRN221_BusinessLogic.Service;
using System.Security.Claims;

namespace PRN221_Client.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public string? Username { get; set; }
        public string? Role { get; set; }
        public string? FullName { get; set; }

        public IndexModel(IHttpContextAccessor httpContextAccessor, ILogger<IndexModel> logger, IAccountService accountService)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _accountService = accountService;
        }

        public async Task<IActionResult> OnGet()
        {
            // Kiểm tra session
            Username = HttpContext.Session.GetString("UserSession");
            Role = HttpContext.Session.GetString("UserRole");

            if(Username != null)
            {
                FullName = await _accountService.GetFullNameByUsername(Username);
            }
            
            return Page();
        }

    }
}
