using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_BusinessLogic.Interface;

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

        public async Task OnGet()
        {
            NewsList = await _newsService.GetAllNews();
            //foreach (var news in NewsList)
            //{
            //    Console.WriteLine(news.Title);
            //}
        }
    }
}
