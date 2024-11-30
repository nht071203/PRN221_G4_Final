using System;
using System.Collections.Generic;

namespace PRN221_Models.Models;

public partial class AccountConversation
{
    public int AccountId { get; set; }

    public int ConversationId { get; set; }

    public bool? IsOut { get; set; }

    public DateTime? OutAt { get; set; }
}
