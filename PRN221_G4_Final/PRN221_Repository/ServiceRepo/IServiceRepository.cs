using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.ServiceRepo
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetAll();
        Task<Service> GetById(int id);
        Task<IEnumerable<Service>> GetServicesPaged(int pageNumber, int pageSize);
        Task<int> GetTotalServicesCount();
        Task<int> CountServicecConfirm(int id);
        Task<IEnumerable<ServiceRating>> GetAllRatingByServiceId(int id);
    }
}
