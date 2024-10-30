using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using PRN221_BusinessLogic.Service;
using PRN221_Models.Models;

namespace PRN221_Client.Pages.Profile
{
    public class UpdateProfileModel : PageModel
    {
        private readonly IAccountService _accountService;

        public UpdateProfileModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public Account Profile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _accountService.GetByIdAccount(id);
            if (account == null)
            {
                return NotFound();
            }
            Profile = account;
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files[0];

                // Gọi Firebase service để upload hình ảnh
                var firebaseService = new FirebaseConfig();
                Profile.Avatar = await firebaseService.UploadToFirebase(file);
            }

            await _accountService.UpdateAccount(Profile);
            return Page();
        }

    }
}
