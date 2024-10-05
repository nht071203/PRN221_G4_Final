﻿using System;
using System.Collections.Generic;

namespace PRN221_Models.Models;

public partial class CategoryService
{
    public int CategoryServiceId { get; set; }

    public string CategoryServiceName { get; set; } = null!;

    public string? CategoryServiceDescription { get; set; }
}
