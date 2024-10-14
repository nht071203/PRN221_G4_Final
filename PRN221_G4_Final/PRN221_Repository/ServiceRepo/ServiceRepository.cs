using PRN221_DataAccess.DAOs;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.ServiceRepo
{
    public class ServiceRepository : IServiceRepository
    {
        private ServiceDAO serviceDAO;
        public ServiceRepository(ServiceDAO item)
        {
            serviceDAO = item;
        }
        public async Task<IEnumerable<Service>> GetAll()
        {
            return await serviceDAO.GetAll();
        }
        public async Task<Service> GetById(int id)
        {
            return await serviceDAO.GetById(id);
        }
        public async Task<IEnumerable<Service>> GetServicesPaged(int pageNumber, int pageSize)
        {
            return await serviceDAO.GetServicesPaged(pageNumber, pageSize);
        }
        public async Task<int> GetTotalServicesCount()
        {
            return await serviceDAO.GetTotalServicesCount();
        }
    }
}
