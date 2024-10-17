using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using PRN221_Client.Pagination;
using System;
namespace PRN221_Client.Pages.News
{
    public class ListNewsModel : PageModel
    {
        private readonly INewsService _newsService;

        public ListNewsModel(INewsService newsService)
        {
            _newsService = newsService;
        }

        public IEnumerable<PRN221_Models.Models.News> NewsList { get; set; }
        public IEnumerable<PRN221_Models.Models.News> NewsListByCategory { get; set; }
        public IEnumerable<PRN221_Models.Models.CategoryNews> CategoryNewsList { get; set; }
        public string SearchKey { get; set; }
        public async Task OnGetAsync(int? cat, string searchKey)
        {
            if (!string.IsNullOrEmpty(searchKey) || cat.HasValue)
            {
                SearchKey = searchKey;
                if (string.IsNullOrEmpty(searchKey) && cat.HasValue)
                {
                    // Gọi phương thức với categoryId.Value
                    NewsListByCategory = await _newsService.GetAllNewsByCategoryId(cat.Value);
                    NewsList = NewsListByCategory;
                }
                else
                {
                    NewsList = await _newsService.SearchNews(cat ?? 0, searchKey);
                    if (NewsList.Count() == 0)
                    {
                        Console.WriteLine("No category or search key provided.");
                        TempData["Message"] = "No category or search key provided.";
                    }
                }
            }
            else
            {
                NewsList = await _newsService.GetAllNews();
            }
            CategoryNewsList = await _newsService.GetCategoriesHaveNews();
        }
        public async Task<string> GetCategoryName(int categoryId)
        {
            var category = await _newsService.GetCategoryNewsById(categoryId);
            return category?.CategoryNewsName ?? "Unknown";
        }
    }
}
