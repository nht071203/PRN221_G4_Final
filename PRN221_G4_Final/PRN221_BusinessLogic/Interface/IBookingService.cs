using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_BusinessLogic.Interface
{
    public interface IBookingService
    {
        Task<PRN221_Models.Models.BookingService> GetBookingById(int id);
        Task<PRN221_Models.Models.BookingService> AddBooking(PRN221_Models.Models.BookingService item);
    }
}
