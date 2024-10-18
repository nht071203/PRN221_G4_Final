using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;

namespace PRN221_Client.Pages.Service
{
    public class DeleteServiceModel : PageModel
    {
		private readonly IRequirementService _requirementService;

		public DeleteServiceModel(IRequirementService requirementService)
		{
			_requirementService = requirementService;
		}
		[BindProperty]
		public PRN221_Models.Models.Service ServiceItem { get; set; }
		public async Task OnGet(int id)
        {
			/*ServiceItem = await _requirementService.GetServiceById(id);
			if (ServiceItem == null)
			{
				RedirectToPage("/Service/ListServiceExpert");
			}

			ServiceItem.IsDeleted = true;

			var serviceDelete = await _requirementService.UpdateService(ServiceItem);
			RedirectToPage("/Service/ListServiceExpert");*/
        }

		public async Task<IActionResult> OnGetRemoveService(int id)
		{
			ServiceItem = await _requirementService.GetServiceById(id);
			Console.WriteLine(id);
			if (ServiceItem == null)
			{
				Console.WriteLine("Xoa that bai");
				return RedirectToPage("/Service/ListServiceExpert");
			}

			ServiceItem.IsDeleted = true;

			var serviceDelete = await _requirementService.UpdateService(ServiceItem);
			return RedirectToPage("/Service/ListServiceExpert");
		}
	}
}
