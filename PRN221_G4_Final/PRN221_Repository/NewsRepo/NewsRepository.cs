using PRN221_DataAccess.DAOs;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.NewsRepo
{
    public class NewsRepository : INewsRepository
    {
        private readonly NewsDAO _newsDAO;

        public NewsRepository(NewsDAO newsDAO)
        {
            _newsDAO = newsDAO;
        }

        public async Task<IEnumerable<News>> GetAllNews()
        {
            return await _newsDAO.GetAllNews();
        }
    }
}
