using Microsoft.EntityFrameworkCore;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_DataAccess.DAOs
{
    public class ServiceDAO : SingletonBase<ServiceDAO>
    {
        public async Task<IEnumerable<Service>> GetAll()
        {
            return await _context.Services.ToListAsync();
        }
        public async Task<Service> GetById(int id)
        {
            var item = await _context.Services.FirstOrDefaultAsync(c => c.ServiceId == id);
            if (item == null) return null;
            return item;
        }

        // Lấy dịch vụ theo phân trang
        public async Task<IEnumerable<Service>> GetServicesPaged(int pageNumber, int pageSize)
        {
            return await _context.Services
                .Where(s => s.IsDeleted == false) // Adjust based on your business logic
                .OrderBy(s => s.ServiceId) // Ensure consistent ordering
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        // Đếm tổng số dịch vụ khả dụng
        public async Task<int> GetTotalServicesCount()
        {
            return await _context.Services.CountAsync(s => s.IsDeleted == false);
        }

        // Đếm tổng số dịch vụ đã được sử dụng
        public async Task<int> CountServicecConfirm(int id)
        {
            return await _context.BookingServices.Where(b => b.ServiceId == id && b.BookingStatus.Equals("confirmed")).CountAsync();
        }

        // Lấy danh sách đánh giá của 1 dịch vụ
        public async Task<IEnumerable<ServiceRating>> GetAllRatingByServiceId(int id)
        {
            return await _context.ServiceRatings.Where(sr => sr.ServiceId == id).ToListAsync();
        }

		// Lấy danh sách dịch vụ theo accountId
		public async Task<IEnumerable<Service>> GetAllServiceByAccId(int id)
		{
			return await _context.Services.Where(c => c.CreatorId == id && c.IsDeleted == false).ToListAsync();
		}

        // Tạo dịch vụ
        public async Task<Service> AddService(Service item)
        {
            _context.Services.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        // Chỉnh sửa dịch vụ
        /*public async Task UpdateService(Service item)
        {
            var existingItem = await GetById(item.ServiceId);
            if (existingItem == null) return;

            _context.Entry(existingItem).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();
        }*/

        public async Task<Service> UpdateService(Service item)
        {
            var existingItem = await GetById(item.ServiceId);
            if (existingItem == null) return null;

            _context.Entry(existingItem).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();
            return item;

        }

        // Xóa dịch vụ
        public async Task DeleteService(int id)
        {
            var item = await GetById(id);
            if (item == null) return;

            _context.Services.Remove(item);
            await _context.SaveChangesAsync();
        }

        // Lấy danh sách dịch vụ không xóa
        public async Task<IEnumerable<Service>> GetAllServiceAvailable()
        {
            return await _context.Services.Where(c => c.IsDeleted == false).ToListAsync();
        }
    }
}
