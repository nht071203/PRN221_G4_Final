using System;
using System.Collections.Generic;

namespace ProjectPRNModel.Models;

public partial class NewsImage
{
    public int NewsImageId { get; set; }

    public int NewsId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public bool? IsDeleted { get; set; }
}
