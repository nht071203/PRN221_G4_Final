using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.BookingRepo
{
    public interface IBookingRepository
    {
        Task<BookingService> GetBookingById(int id);
        Task<BookingService> AddBooking(BookingService item);
    }
}
