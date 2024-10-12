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
    }
}
