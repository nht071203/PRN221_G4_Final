using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
using PRN221_BusinessLogic.Service;
using PRN221_Client.Pagination;
using PRN221_Models.Models;
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
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        private const int PageSize = 8;
        public async Task<IActionResult> OnGetAsync(int? cat, string searchKey, int p = 1)
        {
            CurrentPage = p;

            if (!string.IsNullOrEmpty(searchKey) || cat.HasValue)
            {
                SearchKey = searchKey;

                if (string.IsNullOrEmpty(searchKey) && cat.HasValue)
                {
                    NewsListByCategory = await _newsService.GetAllNewsByCategoryId(cat.Value);

                    NewsList = NewsListByCategory
                                .Skip((p - 1) * PageSize)
                                .Take(PageSize);

                    TotalPages = (int)Math.Ceiling(NewsListByCategory.Count() / (double)PageSize);
                }
                else
                {
                    NewsList = await _newsService.SearchNews(cat ?? 0, searchKey);

                    NewsList = NewsList
                                .Skip((p - 1) * PageSize)
                                .Take(PageSize);

                    TotalPages = (int)Math.Ceiling(NewsList.Count() / (double)PageSize);

                    if (!NewsList.Any())
                    {
                        TempData["Message"] = "No news found with the given category or search key.";
                    }
                }
            }
            else
            {
                NewsList = await _newsService.GetNewsPaged(CurrentPage, PageSize);

                int totalNews = await _newsService.GetTotalNewsCount();
                TotalPages = (int)Math.Ceiling(totalNews / (double)PageSize);
            }

            CategoryNewsList = await _newsService.GetCategoriesHaveNews();
            return Page();
        }
        public async Task<string> GetCategoryName(int categoryId)
        {
            var category = await _newsService.GetCategoryNewsById(categoryId);
            return category?.CategoryNewsName ?? "Unknown";
        }
    }
}
