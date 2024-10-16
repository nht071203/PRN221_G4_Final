using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using PRN221_Models.Models;

namespace PRN221_Client.Pages.Service
{
    public class ServiceDetailModel : PageModel
    {
        private readonly IRequirementService _requirementServices;
        private readonly IAccountService _accountService;
        public ServiceDetailModel(IRequirementService requirementService, IAccountService accountService) 
        {
            _requirementServices = requirementService;
            _accountService = accountService;
        }
        public PRN221_Models.Models.Service ServiceDetail { get; set; }
        public IEnumerable<ServiceRating> ServiceRatingList { get; set; }
        public int CountBookingService { get; set; }
        public Account CreatorService {  get; set; }
        public async Task OnGet(int id)
        {
            if (id == null)
            {
                RedirectToPage("/Index");
            }

            ServiceDetail = await _requirementServices.GetServiceById(id);
            CreatorService = await _accountService.GetByIdAccount(ServiceDetail.CreatorId);
            CountBookingService = await _requirementServices.CountServicecConfirm(ServiceDetail.ServiceId);
            ServiceRatingList = await _requirementServices.GetAllRatingByServiceId(ServiceDetail.ServiceId);

            Console.WriteLine(CountBookingService);

            if (ServiceDetail == null)
            {
                RedirectToPage("/Index");
            }
        }
    }
}
