using PRN221_DataAccess.DAOs;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.CategoryPostRepo
{
    public class CategoryPostRepository : ICategoryPostRepository
    {
        private readonly CategoryPostDAO _categoryPostDAO;

        public CategoryPostRepository(CategoryPostDAO categoryPostDAO)
        {
            _categoryPostDAO = categoryPostDAO;
        }

        public async Task<IEnumerable<CategoryPost>> GetAllCategory()
        {
            return await _categoryPostDAO.GetAll();
        }
        public async Task<CategoryPost> FindById(int id) => await _categoryPostDAO.FindById(id);
        public async Task Add(CategoryPost news) => await _categoryPostDAO.Add(news);
        public async Task Update(CategoryPost news) => await _categoryPostDAO.Update(news);

        public async Task Delete(int id) => await _categoryPostDAO.Delete(id);
    }
}
