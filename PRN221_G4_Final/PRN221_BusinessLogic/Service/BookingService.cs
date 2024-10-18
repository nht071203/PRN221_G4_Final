using PRN221_BusinessLogic.Interface;
using PRN221_Repository.BookingRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_BusinessLogic.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingtService;
        public BookingService(IBookingRepository bookingtService)
        {
            _bookingtService = bookingtService;
        }
        public async Task<PRN221_Models.Models.BookingService> GetBookingById(int id)
        {
            return await _bookingtService.GetBookingById(id);
        }
        public async Task<PRN221_Models.Models.BookingService> AddBooking(PRN221_Models.Models.BookingService item)
        {
            return await _bookingtService.AddBooking(item);
        }
    }
}
