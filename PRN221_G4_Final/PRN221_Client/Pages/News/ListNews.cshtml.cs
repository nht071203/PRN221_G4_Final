using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;
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
        public async Task OnGetAsync(int? id)
        {
            if (id.HasValue)
            {
                // Gọi phương thức với categoryId.Value
                NewsListByCategory = await _newsService.GetAllNewsByCategoryId(id.Value);
                //if (NewsListByCategory != null)
                //{

                //}
                NewsList = NewsListByCategory;
            }
            else
            {
                NewsList = await _newsService.GetAllNews();
            }
            CategoryNewsList = await _newsService.GetCategoriesHaveNews();
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
    }
}
