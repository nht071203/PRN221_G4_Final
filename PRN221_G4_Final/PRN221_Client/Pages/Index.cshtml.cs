using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using PRN221_BusinessLogic.Service;
using System.Security.Claims;
using PRN221_Models.DTO;

namespace PRN221_Client.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPostService _postService;
        public string? Username { get; set; }
        public string? Role { get; set; }
        public string? FullName { get; set; }
        public List<PostDTO> Posts { get; set; }

        public IndexModel(IHttpContextAccessor httpContextAccessor, ILogger<IndexModel> logger, IAccountService accountService, IPostService postService)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _accountService = accountService;
            _postService = postService;
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

            //LAY LIST POST KEM IMAGE
            Posts = await _postService.GetListPostAndImage();
            
            return Page();
        }

    }
}
