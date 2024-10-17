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
		Task<IEnumerable<Service>> GetAllServiceAvailable();
		Task<Service> GetById(int id);
        Task<IEnumerable<Service>> GetServicesPaged(int pageNumber, int pageSize);
        Task<int> GetTotalServicesCount();
        Task<int> CountServicecConfirm(int id);
        Task<IEnumerable<ServiceRating>> GetAllRatingByServiceId(int id);
		Task<IEnumerable<Service>> GetAllServiceByAccId(int id);
        Task<Service> AddService(Service item);
        Task<Service> UpdateService(Service item);
        Task DeleteService(int id);
    }
}
