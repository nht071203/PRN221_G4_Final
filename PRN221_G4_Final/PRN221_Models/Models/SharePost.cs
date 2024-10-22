using System;
using System.Collections.Generic;

namespace PRN221_Models.Models;

public partial class SharePost
{
    public int SharePostId { get; set; }

    public int PostId { get; set; }

    public int SharerId { get; set; }

    public DateTime? ShareAt { get; set; }

    public bool? IsDeleted { get; set; }
}
