using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using PRN221_BusinessLogic.Service;

namespace PRN221_Client.Pages.Service
{
    public class ListServiceExpertModel : PageModel
    {
		private readonly IRequirementService _requirementServices;
		public ListServiceExpertModel(IRequirementService requirementService)
		{
			_requirementServices = requirementService;
		}
		public IEnumerable<PRN221_Models.Models.Service> ServiceByAccIdList { get; set; }
        public async Task OnGet()
        {
            int getAccId = Convert.ToInt32(HttpContext.Session.GetInt32("AccountID"));
			ServiceByAccIdList = await _requirementServices.GetAllServiceByAccId(getAccId);
		}
    }
}
