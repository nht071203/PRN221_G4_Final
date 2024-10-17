using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_BusinessLogic.Interface
{
    public interface IRequirementService
    {
        Task<IEnumerable<PRN221_Models.Models.Service>> GetAllService();
		Task<IEnumerable<PRN221_Models.Models.Service>> GetAllServiceAvailable();
		Task<PRN221_Models.Models.Service> GetServiceById(int id);
        Task<IEnumerable<PRN221_Models.Models.Service>> GetServicesPaged(int pageNumber, int pageSize);
        Task<int> GetTotalServicesCount();
        Task<int> CountServicecConfirm(int id);
        Task<IEnumerable<ServiceRating>> GetAllRatingByServiceId(int id);
		Task<IEnumerable<PRN221_Models.Models.Service>> GetAllServiceByAccId(int id);
        Task<PRN221_Models.Models.Service> AddService(PRN221_Models.Models.Service item);
        //Task UpdateService(PRN221_Models.Models.Service item);
        Task<PRN221_Models.Models.Service> UpdateService(PRN221_Models.Models.Service item);
        Task DeleteService(int id);
    }
}
