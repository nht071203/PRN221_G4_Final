using System;
using System.Collections.Generic;

namespace PRN221_Models.Models;

public partial class ServiceRating
{
    public int RatingId { get; set; }

    public int ServiceId { get; set; }

    public int UserId { get; set; }

    public decimal Rating { get; set; }

    public string Comment { get; set; }

    public DateTime RatedAt { get; set; }
}
