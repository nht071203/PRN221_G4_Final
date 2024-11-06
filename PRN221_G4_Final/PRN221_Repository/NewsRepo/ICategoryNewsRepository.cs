using Microsoft.EntityFrameworkCore;
using PRN221_DataAccess.DAOs;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.NewsRepo
{
    public interface ICategoryNewsRepository
    {
        Task<CategoryNews> GetCategoryNewsById(int id);
        Task<IEnumerable<CategoryNews>> GetAllCategoryNews();
        Task<IEnumerable<CategoryNews>> GetCategoriesHaveNews();
        Task Add(CategoryNews item);
        Task Update(CategoryNews item);
        Task Delete(int id);
    }
}
