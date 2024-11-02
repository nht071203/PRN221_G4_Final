using PRN221_Models.Models;

namespace PRN221_Client.ViewModel
{
    public class MessageListViewModel
    {
        public IEnumerable<Message> Messages { get; set; }
        public int ConversationId { get; set; }
    }
}
