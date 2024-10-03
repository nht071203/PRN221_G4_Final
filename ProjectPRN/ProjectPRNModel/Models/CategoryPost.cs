using System;
using System.Collections.Generic;

namespace ProjectPRNModel.Models;

public partial class CategoryPost
{
    public int CategoryPostId { get; set; }

    public string CategoryPostName { get; set; } = null!;

    public string? CategoryPostDescription { get; set; }
}
