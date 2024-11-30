using PRN221_DataAccess.DAOs;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.NewsRepo
{
    public class CategoryNewsRepository : ICategoryNewsRepository
    {
        private readonly CategoryNewsDAO _categoryNewsDAO;

        public CategoryNewsRepository(CategoryNewsDAO categoryNewsDAO)
        {
            _categoryNewsDAO = categoryNewsDAO;
        }

        public async Task<IEnumerable<CategoryNews>> GetAllCategoryNews() => await _categoryNewsDAO.GetAllCategoryNews();

        public async Task<IEnumerable<CategoryNews>> GetCategoriesHaveNews() => await _categoryNewsDAO.GetCategoriesHaveNews();

        public async Task<CategoryNews> GetCategoryNewsById(int id) => await _categoryNewsDAO.GetCategoryNewsById(id);
        public async Task Add(CategoryNews news) => await _categoryNewsDAO.AddCatNews(news);
        public async Task Update(CategoryNews news) => await _categoryNewsDAO.UpdateCatNews(news);

        public async Task Delete(int id) => await _categoryNewsDAO.DeleteCatNews(id);
    }
}
