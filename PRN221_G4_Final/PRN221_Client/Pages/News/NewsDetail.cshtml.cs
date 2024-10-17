using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using PRN221_BusinessLogic.Service;
using PRN221_Models.Models;

namespace PRN221_Client.Pages.News
{
    public class NewsDetailModel : PageModel
    {
        private readonly INewsService _newsService;

        public NewsDetailModel(INewsService newsService)
        {
            _newsService = newsService;
        }
        public PRN221_Models.Models.News NewsDetail { get; set; }
        public IEnumerable<PRN221_Models.Models.CategoryNews> CategoryNewsList { get; set; }
        public IEnumerable<PRN221_Models.Models.News> List2News { get; set; }
        public CategoryNews CategoryNews { get; set; }
        public string SearchKey { get; set; }
        public async Task<IActionResult> OnGetAsync(int id, string searchKey, int? cat)
        {
            SearchKey = searchKey;
            if (!string.IsNullOrEmpty(searchKey) || cat.HasValue)
            {
                return RedirectToPage("/News/ListNews", new { cat = cat, searchKey = searchKey });
            }
            NewsDetail = await _newsService.GetByIdNews(id);
            if (NewsDetail == null)
            {
                return NotFound();
            }
            CategoryNewsList = await _newsService.GetCategoriesHaveNews();
            CategoryNews = await _newsService.GetCategoryNewsById(NewsDetail.CategoryNewsId);
            List2News = await _newsService.GetAllNews();
            List2News = List2News.OrderByDescending(n => n.CreatedAt).Take(2);
            return Page();
        }
    }
}
