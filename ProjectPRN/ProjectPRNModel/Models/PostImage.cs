using System;
using System.Collections.Generic;

namespace ProjectPRNModel.Models;

public partial class PostImage
{
    public int PostImageId { get; set; }

    public int PostId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public bool? IsDeleted { get; set; }
}
