﻿using System;
using System.Collections.Generic;

namespace PRN221_Models.Models;

public partial class LikePost
{
    public int LikePostId { get; set; }

    public int PostId { get; set; }

    public int AccountId { get; set; }

    public int? UnLike { get; set; }
}