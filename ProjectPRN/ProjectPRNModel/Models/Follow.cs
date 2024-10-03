using System;
using System.Collections.Generic;

namespace ProjectPRNModel.Models;

public partial class Follow
{
    public int FollowId { get; set; }

    public int FollowerId { get; set; }

    public int BeFollowedId { get; set; }

    public DateOnly FollowAt { get; set; }
}
