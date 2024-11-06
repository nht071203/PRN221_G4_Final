using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using System.ComponentModel.DataAnnotations;

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
		public PRN221_Models.Models.Service ChangeStatusService { get; set; }
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
            if (ServiceItem.Price <= 0)
            {
                ModelState.AddModelError("PriceNotZero", "Giá phải lớn hơn 0");
            }
            else if (ServiceItem.Price % 1 != 0)
            {
                Console.WriteLine("In loi");
                ModelState.AddModelError("PriceInteger", "Giá phải là số nguyên.");
            }

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

		public async Task<IActionResult> OnGetDisableService(int id)
		{
			ChangeStatusService = await _requirementService.GetServiceById(id);
			Console.WriteLine(id);
			if (ChangeStatusService == null)
			{
				Console.WriteLine("Đổi trạng thái that bai");
				return RedirectToPage("/Service/ListServiceExpert");
			}

			ChangeStatusService.IsEnable = false;

			var serviceDelete = await _requirementService.UpdateService(ChangeStatusService);
			return RedirectToPage("/Service/ListServiceExpert");
		}

		public async Task<IActionResult> OnGetEnableService(int id)
		{
			ChangeStatusService = await _requirementService.GetServiceById(id);
			Console.WriteLine(id);
			if (ChangeStatusService == null)
			{
				Console.WriteLine("Đổi trạng thái that bai");
				return RedirectToPage("/Service/ListServiceExpert");
			}

			ChangeStatusService.IsEnable = true;

			var serviceDelete = await _requirementService.UpdateService(ChangeStatusService);
			return RedirectToPage("/Service/ListServiceExpert");
		}
	}
}
