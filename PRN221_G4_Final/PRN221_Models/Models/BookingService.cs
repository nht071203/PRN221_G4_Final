using System;
using System.Collections.Generic;

namespace PRN221_Models.Models;

public partial class BookingService
{
    public int BookingId { get; set; }

    public int ServiceId { get; set; }

    public int BookingBy { get; set; }

    public DateTime? BookingAt { get; set; }

    public string BookingStatus { get; set; }

    public bool? IsDeletedFarmer { get; set; }

    public bool? IsDeletedExpert { get; set; }

    public string Content { get; set; }
}
