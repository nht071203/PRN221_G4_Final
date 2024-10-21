using System;
using System.Collections.Generic;

namespace PRN221_Models.Models;

public partial class Service
{
    public int ServiceId { get; set; }

    public int CreatorId { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public double Price { get; set; }

    public bool? IsEnable { get; set; }

    public bool? IsDeleted { get; set; }

    public decimal? AverageRating { get; set; }

    public int? RatingCount { get; set; }
}
