using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using PRN221_Models.Models;

namespace PRN221_Client.Pages.News
{
    public class IndexModel : PageModel
    {
        private readonly INewsService _newsService;

        public IndexModel(INewsService newsService)
        {
            _newsService = newsService;
        }

        public IEnumerable<PRN221_Models.Models.News> NewsList { get; set; }
        public CategoryNews categoryNews { get; set; }
        public async Task OnGet()
        {
            NewsList = await _newsService.GetAllNews();
            //foreach (var news in NewsList)
            //{
            //    Console.WriteLine(news.Title);
            //}
        }

        public async Task<string> GetCategoryName(int categoryId)
        {
            var category = await _newsService.GetCategoryNewsById(categoryId);
            return category?.CategoryNewsName ?? "Unknown"; // Replace 'Name' with the actual property name for category
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var news = await _newsService.GetByIdNews(id);
            if (news != null)
            {
                //news.IsDeleted = true;
                await _newsService.DeleteNews(id);
            }

            return RedirectToPage(); // T?i l?i trang sau khi xóa
        }

        public async Task<IActionResult> OnPostRestore(int id)
        {
            var news = await _newsService.GetByIdNews(id);
            if (news != null)
            {
                news.IsDeleted = false;
                news.DeletedAt = null;
                //news.IsDeleted = true;
                await _newsService.UpdateNews(news);
            }

            return RedirectToPage(); // T?i l?i trang sau khi xóa
        }
        //public async Task<JsonResult> OnPostDeleteAjax(int id)
        //{
        //    var news = await _newsService.GetByIdNews(id);
        //    if (news != null)
        //    {
        //        news.IsDeleted = true;
        //        await _newsService.DeleteNews(news);
        //        return new JsonResult(new { success = true });
        //    }
        //    return new JsonResult(new { success = false, message = "News item not found" });
        //}

    }
}
