using System;
using System.Collections.Generic;

namespace ProjectPRNModel.Models;

public partial class View
{
    public int CountViewId { get; set; }

    public int AccountId { get; set; }

    public int? PostId { get; set; }

    public int? NewsId { get; set; }
}
