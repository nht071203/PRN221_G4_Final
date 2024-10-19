using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using PRN221_BusinessLogic.Service;
using PRN221_Models.Models;
using System.ComponentModel.DataAnnotations;

namespace PRN221_Client.Pages.Service
{
    public class ServiceDetailModel : PageModel
    {
        private readonly IRequirementService _requirementServices;
        private readonly IAccountService _accountService;
        private readonly IBookingService _bookingService;
        private readonly IRateService _rateService;
        public ServiceDetailModel(IRequirementService requirementService, IAccountService accountService, IBookingService bookingService, IRateService rateService) 
        {
            _requirementServices = requirementService;
            _accountService = accountService;
            _bookingService = bookingService;
            _rateService = rateService;
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
                return RedirectToPage("/Service/ServiceDetail", new { id = InputServiceId });
            }

            //HttpContext.Session.SetString("UserSession", getAccount.Username);

            return RedirectToPage("/Service/BookingSuccess");
        }

        [BindProperty]
        public decimal RatingPoint { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Cần bạn đóng góp ý kiến")]
        public string CommentService {  get; set; }
        public async Task<IActionResult> OnPostRateService()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("AccountID"));

            ServiceRating newRating = new ServiceRating
            {
                ServiceId = InputServiceId,
                UserId = userId,
                Rating = RatingPoint,
                Comment = CommentService,
                RatedAt = DateTime.Now
            };

            var createRate = await _rateService.AddRating(newRating);

            if (createRate != null)
            {
                var getServiceUpdate = await _requirementServices.GetServiceById(InputServiceId);

                IEnumerable<ServiceRating> getAllRating = await _requirementServices.GetAllRatingByServiceId(InputServiceId);

                decimal sumRate = 0;

                decimal count = 0;

                foreach (ServiceRating rating in getAllRating)
                {
                    sumRate += rating.Rating;
                    count++;
                }

                decimal avgRate = count > 0 ? Math.Round(sumRate / count, 1) : 0;

                getServiceUpdate.RatingCount += 1;
                getServiceUpdate.AverageRating = avgRate;

                var updateServceRate = await _requirementServices.UpdateService(getServiceUpdate);
            }

            return RedirectToPage("/Service/ServiceDetail", new { id = InputServiceId });

        }
    }
}
