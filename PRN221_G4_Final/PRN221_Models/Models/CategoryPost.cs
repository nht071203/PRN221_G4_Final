using System;
using System.Collections.Generic;

namespace PRN221_Models.Models;

public partial class CategoryPost
{
    public int CategoryPostId { get; set; }

    public string CategoryPostName { get; set; } = null!;

    public string? CategoryPostDescription { get; set; }
}
