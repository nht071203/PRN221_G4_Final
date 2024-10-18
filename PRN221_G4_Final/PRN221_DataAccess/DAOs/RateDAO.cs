using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_DataAccess.DAOs
{
    public class RateDAO : SingletonBase<RateDAO>
    {
        // Tạo đánh giá
        public async Task<ServiceRating> AddRating(ServiceRating item)
        {
            _context.ServiceRatings.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
