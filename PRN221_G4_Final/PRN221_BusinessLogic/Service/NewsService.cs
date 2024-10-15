using PRN221_BusinessLogic.Interface;
using PRN221_Models.Models;
using PRN221_Repository.NewsRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_BusinessLogic.Service
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepo;

        public NewsService(INewsRepository newsRepo)
        {
            _newsRepo = newsRepo;
        }

        public async Task<IEnumerable<News>> GetAllNews() => await _newsRepo.GetAllNews();
        public async Task<News> GetByIdNews(int id)
        {
            return await _newsRepo.GetById(id);
        }
        public async Task AddNews(News item)
        {
            await _newsRepo.Add(item);
        }
        public async Task UpdateNews(News item)
        {
            await _newsRepo.Update(item);
        }
        public async Task DeleteNews(int id)
        {
            await _newsRepo.Delete(id);
        }

        public async Task<CategoryNews> GetCategoryNewsById(int id)
        {
            return await _newsRepo.GetCategoryNewsById(id);
        }


        public async Task<int> GetTotalNewsService()
        {
            return await _newsRepo.GetTotalNewsRepo();
        }
        public async Task<IEnumerable<CategoryNews>> GetAllCategoryNews()
        {
            return await _newsRepo.GetAllCategoryNews();

        }
    }
}
