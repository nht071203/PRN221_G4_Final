using System;
using System.Collections.Generic;

namespace PRN221_Models.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public int? AccountId { get; set; }

    public int? PostId { get; set; }

    public string? Content { get; set; }

    public int? Rate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsDeleted { get; set; }
}
