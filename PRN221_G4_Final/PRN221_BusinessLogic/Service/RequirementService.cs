using PRN221_BusinessLogic.Interface;
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
    }
}
