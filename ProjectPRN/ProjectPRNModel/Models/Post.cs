using System;
using System.Collections.Generic;

namespace ProjectPRNModel.Models;

public partial class Post
{
    public int PostId { get; set; }

    public int CategoryPostId { get; set; }

    public int? AccountId { get; set; }

    public DateOnly CreatedAt { get; set; }

    public DateOnly? UpdateAt { get; set; }

    public string? PostContent { get; set; }

    public bool? IsDeleted { get; set; }

    public DateOnly? DeletedAt { get; set; }
}
