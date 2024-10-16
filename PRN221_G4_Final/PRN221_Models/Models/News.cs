using System;
using System.Collections.Generic;

namespace PRN221_Models.Models;

public partial class News
{
    public int NewsId { get; set; }

    public int CategoryNewsId { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public DateOnly CreatedAt { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public DateOnly? DeletedAt { get; set; }

    public bool? IsDeleted { get; set; }
}
