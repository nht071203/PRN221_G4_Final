using System;
using System.Collections.Generic;

namespace PRN221_Models.Models;

public partial class Message
{
    public int MessageId { get; set; }

    public int ConversationId { get; set; }

    public int SenderId { get; set; }

    public DateTime? CreateAt { get; set; }

    public string Content { get; set; }

    public bool? IsDeleted { get; set; }
}
