using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_BusinessLogic.Interface
{
    public interface INewsService
    {
        Task<IEnumerable<News>> GetAllNews();
        Task<IEnumerable<CategoryNews>> GetAllCategoryNews();
        Task<News> GetByIdNews(int id);
        Task AddNews(News item);
        Task UpdateNews(News item);
        Task DeleteNews(int id);
        Task<CategoryNews> GetCategoryNewsById(int id);
        Task<int> GetTotalNewsService();
    }
}
