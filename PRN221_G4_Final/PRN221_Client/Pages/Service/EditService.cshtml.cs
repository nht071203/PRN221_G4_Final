using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;

namespace PRN221_Client.Pages.Service
{
    public class EditServiceModel : PageModel
    {
        private readonly IRequirementService _requirementService;

        public EditServiceModel(IRequirementService requirementService)
        {
            _requirementService = requirementService;
        }
        [BindProperty]
        public PRN221_Models.Models.Service ServiceItem { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            ServiceItem = await _requirementService.GetServiceById(id);

            if (ServiceItem == null)
            {
                return NotFound();
            }

            return Page();
        }
        public async Task<IActionResult> OnPostUpdateService()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            int getAccId = Convert.ToInt32(HttpContext.Session.GetInt32("AccountID"));

            ServiceItem.CreatorId = getAccId;
            ServiceItem.UpdatedAt = DateTime.Now;
            ServiceItem.IsDeleted = false;

            var updateAcc = await _requirementService.UpdateService(ServiceItem);

            Console.WriteLine(updateAcc.Title);

            if (updateAcc == null)
            {
                Console.WriteLine("Cap nhat that bai");
                return Page();
            }

            return RedirectToPage("/Service/ListServiceExpert"); // Redirect to the list page after updating
        }
    }
}
