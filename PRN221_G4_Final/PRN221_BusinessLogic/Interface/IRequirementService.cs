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
        Task<PRN221_Models.Models.Service> GetServiceById(int id);
        Task<IEnumerable<PRN221_Models.Models.Service>> GetServicesPaged(int pageNumber, int pageSize);
        Task<int> GetTotalServicesCount();
        Task<int> CountServicecConfirm(int id);
        Task<IEnumerable<ServiceRating>> GetAllRatingByServiceId(int id);
    }
}
