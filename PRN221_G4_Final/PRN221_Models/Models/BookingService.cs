using System;
using System.Collections.Generic;

namespace PRN221_Models.Models;

public partial class BookingService
{
    public int BookingId { get; set; }

    public int ServiceId { get; set; }

    public int BookingBy { get; set; }

    public DateOnly BookingAt { get; set; }

    public string BookingStatus { get; set; } = null!;

    public bool? IsDeletedFarmer { get; set; }

    public int? Rating { get; set; }

    public bool? IsDeletedExpert { get; set; }
}
