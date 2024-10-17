using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;

namespace PRN221_Client.Pages.Service
{
    [BindProperties]
    public class AddServiceModel : PageModel
    {
        private readonly IRequirementService _requirementServices;
        public AddServiceModel(IRequirementService requirementService)
        {
            _requirementServices = requirementService;
        }
        public void OnGet()
        {
        }
        [BindProperty]
        public string TitleInput {  get; set; }
        [BindProperty]
        public double PriceInput { get; set; }
        [BindProperty]
        public string Description { get; set; }
        public async Task<IActionResult> OnPostAddService() {
            int getAccId = Convert.ToInt32(HttpContext.Session.GetInt32("AccountID"));

            var service = new PRN221_Models.Models.Service
            {
                ServiceId = 0,
                CreatorId = getAccId,
                CreateAt = DateOnly.FromDateTime(DateTime.Now), // Convert DateTime to DateOnly
                UpdatedAt = null,
                DeletedAt = null,
                Title = TitleInput,
                Content = Description,
                Price = PriceInput,
                AverageRating = 0,
                RatingCount = 0,
                IsEnable = true,
                IsDeleted = false
            };

            var addService = await _requirementServices.AddService(service);
            if (addService == null)
            {
                Console.WriteLine("Tạo dịch vụ thất bại!");
                RedirectToPage("/Service/AddService");
            }

            return RedirectToPage("/Service/ListServiceExpert");
        }
    }
}
