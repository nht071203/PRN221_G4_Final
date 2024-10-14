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
    }
}
