using System;
using System.Collections.Generic;

namespace PRN221_Models.Models;

public partial class Follow
{
    public int FollowId { get; set; }

    public int FollowerId { get; set; }

    public int BeFollowedId { get; set; }

    public DateTime? FollowAt { get; set; }
}
