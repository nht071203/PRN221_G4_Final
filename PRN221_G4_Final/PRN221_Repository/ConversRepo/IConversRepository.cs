using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.ConversRepo
{
    public interface IConversRepository
    {
        Task<IEnumerable<Conversation>> GetAllConversationByAccId(int accId);
        Task<AccountConversation> GetAccConversationByAccOId(int converId, int accId);
        Task<IEnumerable<Message>> GetAllMessageByConvId(int converId);
        Task<Message> AddMessage(Message item);
    }
}
