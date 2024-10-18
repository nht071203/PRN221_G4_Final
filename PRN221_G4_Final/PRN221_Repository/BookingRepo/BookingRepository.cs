using PRN221_DataAccess.DAOs;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.BookingRepo
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDAO _bookingDAO;
        public BookingRepository(BookingDAO bookingDAO)
        {
            _bookingDAO = bookingDAO;
        }
        public async Task<BookingService> GetBookingById(int id)
        {
            return await _bookingDAO.GetBookingById(id);
        }
        public async Task<BookingService> AddBooking(BookingService item)
        {
            return await _bookingDAO.AddBooking(item);
        }
    }
}
