using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using PRN221_BusinessLogic.Service;
using PRN221_Models.Models;

namespace PRN221_Client.Pages.Service
{
    public class ServiceDetailModel : PageModel
    {
        private readonly IRequirementService _requirementServices;
        private readonly IAccountService _accountService;
        private readonly IBookingService _bookingService;
        public ServiceDetailModel(IRequirementService requirementService, IAccountService accountService, IBookingService bookingService) 
        {
            _requirementServices = requirementService;
            _accountService = accountService;
            _bookingService = bookingService;
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

        [BindProperty]
        public string InputRequestContent { get; set; }
        [BindProperty]
        public int InputServiceId { get; set; }
        public async Task<IActionResult> OnPostRequestService()
        {
            int getAccId = Convert.ToInt32(HttpContext.Session.GetInt32("AccountID"));
            var getAccount = await _accountService.GetByIdAccount(getAccId);

            PRN221_Models.Models.BookingService newBooking = new PRN221_Models.Models.BookingService
            {
                ServiceId = InputServiceId,
                BookingBy = getAccId,
                BookingAt = DateOnly.FromDateTime(DateTime.Now),
                BookingStatus = "sending",
                IsDeletedFarmer = false,
                Content = InputRequestContent,
                IsDeletedExpert = false,
            };

            var addBooking = await _bookingService.AddBooking(newBooking);

            if (addBooking == null)
            {
                Console.WriteLine("Book lịch thất bại");
                return RedirectToPage($"/Service/ServiceDetail?id={InputServiceId}");
            }

            HttpContext.Session.SetString("UserSession", getAccount.Username);

            Console.WriteLine("Book thành công");

            return RedirectToPage("/Service/BookingSuccess");
        }
    }
}
