using System;
using System.Collections.Generic;

namespace PRN221_Models.Models;

public partial class News
{
    public int NewsId { get; set; }

    public int CategoryNewsId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool? IsDeleted { get; set; }
}
