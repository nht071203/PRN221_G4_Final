using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using System.ComponentModel.DataAnnotations;
using static System.Net.WebRequestMethods;
using System.Security.Principal;

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
        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(200, ErrorMessage = "Quá 200 ký tự")]
        public string TitleInput {  get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Không được để trống")]
        public double PriceInput { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Không được để trống")]
        public string Description { get; set; }
        public async Task<IActionResult> OnPostAddService() {
            int getAccId = Convert.ToInt32(HttpContext.Session.GetInt32("AccountID"));

            if (PriceInput <= 0)
            {
                ModelState.AddModelError("PriceNotZero", "Giá phải lớn hơn 0");
            } else if (PriceInput % 1 != 0)
            {
                Console.WriteLine("In loi");
                ModelState.AddModelError("PriceInteger", "Giá phải là số nguyên.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var service = new PRN221_Models.Models.Service
            {
                ServiceId = 0,
                CreatorId = getAccId,
                CreateAt = DateTime.Now, // Convert DateTime to DateOnly
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


