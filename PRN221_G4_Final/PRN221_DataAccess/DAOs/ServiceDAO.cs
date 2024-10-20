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
                .Where(s => s.IsDeleted == false && s.IsEnable == true) // Adjust based on your business logic
                .OrderBy(s => s.ServiceId) // Ensure consistent ordering
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        // Đếm tổng số dịch vụ khả dụng
        public async Task<int> GetTotalServicesCount()
        {
            return await _context.Services.CountAsync(s => s.IsDeleted == false && s.IsEnable == true); 
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

            item.DeletedAt = DateOnly.FromDateTime(DateTime.Now);
            item.IsDeleted = true;

            _context.Entry(item).CurrentValues.SetValues(item);
            //_context.Services.Remove(item);
            await _context.SaveChangesAsync();
        }

        // Lấy danh sách dịch vụ không xóa
        public async Task<IEnumerable<Service>> GetAllServiceAvailable()
        {
            return await _context.Services.Where(c => c.IsDeleted == false && c.IsEnable == true).ToListAsync();
        }

        //// Tìm kiếm dịch vụ theo tiền
        //// Giá cao nhất
        //public async Task<IEnumerable<Service>> GetAllServicePriceHighest()
        //{
        //    return await _context.Services.Where(s => s.IsDeleted == false && s.IsEnable == true).OrderByDescending(s => s.Price).ToListAsync();
        //}
        //// Giá thấp nhất
        //public async Task<IEnumerable<Service>> GetAllServicePriceLowest()
        //{
        //    return await _context.Services.Where(s => s.IsDeleted == false && s.IsEnable == true).OrderBy(s => s.Price).ToListAsync();
        //}

        //// Tìm kiếm dịch vụ theo số sao
        //public async Task<IEnumerable<Service>> GetAllServiceByRate(decimal lowRate, decimal highRate)
        //{
        //    return await _context.Services.Where(s => s.IsDeleted == false && s.IsEnable == true && (s.AverageRating ?? 0) >= lowRate && (s.AverageRating ?? 0) <= highRate).ToListAsync();
        //}
        //// Tìm dịch vụ đánh giá cao nhất
        //public async Task<IEnumerable<Service>> GetAllServiceByRateInTop()
        //{
        //    return await _context.Services.Where(s => s.IsDeleted == false && s.IsEnable == true && (s.AverageRating ?? 0) == 5).ToListAsync();
        //}
    }
}
