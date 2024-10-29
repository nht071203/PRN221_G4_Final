using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OxyPlot;
using PRN221_BusinessLogic.Interface;
using PRN221_BusinessLogic.Service;
using PRN221_Models.Models;

namespace PRN221_Client.Pages.Service
{
    [Authorize]
    public class ListServicesModel : PageModel
    {
        private readonly IRequirementService _requirementServices;
        private readonly IAccountService _accountService;
        private readonly IBookingService _bookingService;
        private const int PageSize = 2; // Số dịch vụ trong 1 page

        public ListServicesModel(IRequirementService requirementService, IAccountService accountService, IBookingService bookingService)
        {
            _requirementServices = requirementService;
            _accountService = accountService;
            _bookingService = bookingService;
        }
        // Khai báo list chứa các dịch vụ
        public IEnumerable<PRN221_Models.Models.Service> ServiceList { get; set; }

        // Dictionary để lưu tài khoản của từng dịch vụ
        public Dictionary<int, Account?> ServiceCreatorAccounts { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public async Task OnGet()
        {
            int page = 1;

            // Lấy danh sách tất cả dịch vụ
            var allServices = await _requirementServices.GetAllServiceAvailable();

            // Tính tổng số trang
            int totalServices = allServices.Count();
            TotalPages = (int)Math.Ceiling(totalServices / (double)PageSize);

            // Thiết lập trang hiện tại
            CurrentPage = page;

            // Lấy các dịch vụ đã khai báo cho trang hiện tại
            ServiceList = allServices.Skip((page - 1) * PageSize).Take(PageSize);

            // Khởi tạo dictionary để lưu tài khoản của người tạo dịch vụ
            ServiceCreatorAccounts = new Dictionary<int, Account?>();

            foreach (var service in ServiceList)
            {
                var account = await _accountService.GetByIdAccount(service.CreatorId);
                ServiceCreatorAccounts[service.ServiceId] = account;
            }
        }

        //Hàm xử lý chuyển phân trang
        public async Task<IActionResult> OnGetPagination(int p)
        {
            CurrentPage = p;

            // Lấy các dịch vụ theo số trang
            ServiceList = await _requirementServices.GetServicesPaged(p, PageSize);

            // Tính tổng dịch vụ
            int totalServices = await _requirementServices.GetTotalServicesCount();
            TotalPages = (int)Math.Ceiling(totalServices / (double)PageSize);

            ServiceCreatorAccounts = new Dictionary<int, Account?>();

            foreach (var service in ServiceList)
            {
                var account = await _accountService.GetByIdAccount(service.CreatorId);
                ServiceCreatorAccounts[service.ServiceId] = account;
            }

            return Page(); // Trả về trang đã cập nhật
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
                BookingAt = DateTime.Now,
                BookingStatus = "sending",
                IsDeletedFarmer = false,
                Content = InputRequestContent,
                IsDeletedExpert = false,
            };

            var addBooking = await _bookingService.AddBooking(newBooking);

            if (addBooking == null)
            {
                Console.WriteLine("Book lịch thất bại");
                return RedirectToPage("/Service/ListServices");
            }

            HttpContext.Session.SetString("UserSession", getAccount.Username);

            Console.WriteLine("Book thành công");

            return RedirectToPage("/Service/BookingSuccess");
        }
    }
}
