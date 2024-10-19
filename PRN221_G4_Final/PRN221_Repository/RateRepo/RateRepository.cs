using PRN221_DataAccess.DAOs;
using PRN221_Models.Models;
using PRN221_Repository.ServiceRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.RateRepo
{
    public class RateRepository : IRateRepository
    {
        private readonly RateDAO _rateDAO;
        public RateRepository(RateDAO rateDAO)
        {
            _rateDAO = rateDAO;
        }
        public async Task<ServiceRating> AddRating(ServiceRating item)
        {
            return await _rateDAO.AddRating(item);  
        }
    }
}
