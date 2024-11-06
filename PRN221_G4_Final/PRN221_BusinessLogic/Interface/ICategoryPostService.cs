using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_BusinessLogic.Interface
{
    public interface ICategoryPostService
    {
        Task<IEnumerable<CategoryPost>> GetAllCategory();
        Task<CategoryPost> FindById(int id);

        Task AddCatPost(CategoryPost item);
        Task UpdateCatPost(CategoryPost item);
        Task DeleteCatPost(int id);

    }

}
