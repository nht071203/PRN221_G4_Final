using PRN221_BusinessLogic.Interface;
using PRN221_DataAccess.DAOs;
using PRN221_Models.Models;
using PRN221_Repository.ServiceRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_BusinessLogic.Service
{
    public class RequirementService : IRequirementService
    {
        private IServiceRepository _requirementService;
        public RequirementService(IServiceRepository requirementRepository)
        {
            _requirementService = requirementRepository;
        }
        public async Task<IEnumerable<PRN221_Models.Models.Service>> GetAllService()
        {
            return await _requirementService.GetAll();
        }
		public async Task<IEnumerable<PRN221_Models.Models.Service>> GetAllServiceAvailable()
		{
			return await _requirementService.GetAllServiceAvailable();
		}
		public async Task<PRN221_Models.Models.Service> GetServiceById(int id)
        {
            return await _requirementService.GetById(id);
        }
        public async Task<IEnumerable<PRN221_Models.Models.Service>> GetServicesPaged(int pageNumber, int pageSize)
        {
            return await _requirementService.GetServicesPaged(pageNumber, pageSize);
        }
        public async Task<int> GetTotalServicesCount()
        {
            return await _requirementService.GetTotalServicesCount();
        }
        public async Task<int> CountServicecConfirm(int id)
        {
            return await _requirementService.CountServicecConfirm(id);
        }
        public async Task<IEnumerable<ServiceRating>> GetAllRatingByServiceId(int id)
        {
            return await _requirementService.GetAllRatingByServiceId(id);
        }
		public async Task<IEnumerable<PRN221_Models.Models.Service>> GetAllServiceByAccId(int id)
		{
			return await _requirementService.GetAllServiceByAccId(id);
		}
        public async Task<PRN221_Models.Models.Service> AddService(PRN221_Models.Models.Service item)
        {
            return await _requirementService.AddService(item);
        }
        /*public async Task UpdateService(PRN221_Models.Models.Service item)
        {
            await _requirementService.UpdateService(item);
        }*/
        public async Task<PRN221_Models.Models.Service> UpdateService(PRN221_Models.Models.Service item)
        {
            return await _requirementService.UpdateService(item);
        }
        public async Task DeleteService(int id)
        {
            await _requirementService.DeleteService(id);
        }
    }
}
