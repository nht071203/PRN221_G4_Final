using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_BusinessLogic.Interface
{
    public interface IConversService
    {
        Task<IEnumerable<Conversation>> GetAllConversationByAccId(int accId);
        Task<AccountConversation> GetAccConversationByAccOId(int converId, int accId);
        Task<IEnumerable<Message>> GetAllMessageByConvId(int convId);
        Task<Message> AddMessage(Message item);
    }
}
