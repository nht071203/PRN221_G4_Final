﻿using PRN221_DataAccess.DAOs;
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
		public async Task<IEnumerable<Service>> GetAllServiceAvailable()
		{
			return await serviceDAO.GetAllServiceAvailable();
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
        public async Task<int> CountServicecConfirm(int id)
        {
            return await serviceDAO.CountServicecConfirm(id);
        }
        public async Task<IEnumerable<ServiceRating>> GetAllRatingByServiceId(int id)
        {
            return await serviceDAO.GetAllRatingByServiceId(id);
        }
		public async Task<IEnumerable<Service>> GetAllServiceByAccId(int id)
		{
			return await serviceDAO.GetAllServiceByAccId(id);
		}
        public async Task<Service> AddService(Service item)
        {
            return await serviceDAO.AddService(item);
        }
        /*public async Task UpdateService(Service item)
        {
            await serviceDAO.UpdateService(item);
        }*/
        public async Task<Service> UpdateService(Service item)
        {
            return await serviceDAO.UpdateService(item);
        }
        public async Task DeleteService(int id)
        {
            await serviceDAO.DeleteService(id);
        }

    }
}
