using PRN221_BusinessLogic.Interface;
using PRN221_Models.Models;
using PRN221_Repository.CategoryPostRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_BusinessLogic.Service
{
    public class CategoryPostService : ICategoryPostService
    {
        private readonly ICategoryPostRepository _categoryPostRepository;
        public CategoryPostService(ICategoryPostRepository categoryPostRepository)
        {
            _categoryPostRepository = categoryPostRepository;
        }

        public async Task AddCatPost(CategoryPost item)
        {
            await _categoryPostRepository.Add(item);
        }

        public async Task DeleteCatPost(int id)
        {
            await _categoryPostRepository.Delete(id);
        }

        public async Task<CategoryPost> FindById(int id)
        {
            return await _categoryPostRepository.FindById(id);
        }

        public async Task<IEnumerable<CategoryPost>> GetAllCategory()
        {
            return await _categoryPostRepository.GetAllCategory();
        }

        public async Task UpdateCatPost(CategoryPost item)
        {
           await _categoryPostRepository.Update(item);
        }
    }
}
