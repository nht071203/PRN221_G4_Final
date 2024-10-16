using System;
using System.Collections.Generic;

namespace PRN221_Models.Models;

public partial class NewsImage
{
    public int NewsImageId { get; set; }

    public int NewsId { get; set; }

    public string ImageUrl { get; set; }

    public bool? IsDeleted { get; set; }
}
