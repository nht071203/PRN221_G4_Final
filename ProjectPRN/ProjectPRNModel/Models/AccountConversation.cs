using System;
using System.Collections.Generic;

namespace ProjectPRNModel.Models;

public partial class AccountConversation
{
    public int AccountId { get; set; }

    public int ConversationId { get; set; }

    public bool? IsOut { get; set; }

    public DateOnly? OutAt { get; set; }
}
