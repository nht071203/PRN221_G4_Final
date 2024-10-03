using Microsoft.AspNetCore.Mvc;

namespace ProjectPRNClient.Controllers
{
    public class FarmerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListService()
        {
            return View();
        }
    }
}
