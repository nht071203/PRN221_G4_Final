using System;
using System.Collections.Generic;

namespace ProjectPRNModel.Models;

public partial class CategoryNews
{
    public int CategoryNewsId { get; set; }

    public string CategoryNewsName { get; set; } = null!;

    public string? CategoryNewsDescription { get; set; }
}
