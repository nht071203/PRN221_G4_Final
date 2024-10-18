using Microsoft.EntityFrameworkCore;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_DataAccess.DAOs
{
    public class BookingDAO : SingletonBase<BookingDAO>
    {
        public async Task<BookingService> GetBookingById(int id)
        {
            var item = await _context.BookingServices.FirstOrDefaultAsync(c => c.ServiceId == id);
            if (item == null) return null;
            return item;
        }
        // Tạo yêu cầu
        public async Task<BookingService> AddBooking(BookingService item)
        {
            _context.BookingServices.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
