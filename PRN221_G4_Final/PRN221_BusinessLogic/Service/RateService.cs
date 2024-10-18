using PRN221_BusinessLogic.Interface;
using PRN221_Models.Models;
using PRN221_Repository.RateRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_BusinessLogic.Service
{
    public class RateService : IRateService
    {
        private readonly IRateRepository _rateService;
        public RateService(IRateRepository rateService)
        {
            _rateService = rateService;
        }
        public async Task<ServiceRating> AddRating(ServiceRating item)
        {
            return await _rateService.AddRating(item);
        }
    }
}
