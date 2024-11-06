using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRN221_Models.Models;

public partial class Service
{
    public int ServiceId { get; set; }

    public int CreatorId { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
    [Required(ErrorMessage = "Không được để trống")]
    [StringLength(200, ErrorMessage = "Quá 200 ký tự")]
    public string Title { get; set; } = null!;
    [Required(ErrorMessage = "Không được để trống")]
    public string Content { get; set; } = null!;
    [Required(ErrorMessage = "Không được để trống")]
    public double Price { get; set; }

    public bool? IsEnable { get; set; }

    public bool? IsDeleted { get; set; }

    public decimal? AverageRating { get; set; }

    public int? RatingCount { get; set; }
}
