using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.CategoryPostRepo
{
    public interface ICategoryPostRepository
    {
        Task<IEnumerable<CategoryPost>> GetAllCategory();
        Task<CategoryPost> FindById(int id);
        Task Add(CategoryPost item);
        Task Update(CategoryPost item);
        Task Delete(int id);
    }
    
}
