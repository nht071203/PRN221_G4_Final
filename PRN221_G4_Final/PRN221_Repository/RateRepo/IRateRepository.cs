using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.RateRepo
{
    public interface IRateRepository
    {
        Task<ServiceRating> AddRating(ServiceRating item);
    }
}
