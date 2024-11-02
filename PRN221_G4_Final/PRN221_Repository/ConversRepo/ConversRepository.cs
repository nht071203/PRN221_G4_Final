using PRN221_DataAccess.DAOs;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.ConversRepo
{
    public class ConversRepository : IConversRepository
    {
        private ConversationDAO conversationDAO;

        public ConversRepository(ConversationDAO item)
        {
            conversationDAO = item;
        }
        public async Task<IEnumerable<Conversation>> GetAllConversationByAccId(int accId)
        {
            return await conversationDAO.GetAllConversationByAccId(accId);
        }
        public async Task<AccountConversation> GetAccConversationByAccOId(int conversId, int accId) => await conversationDAO.GetAccConversationByAccOId(conversId, accId);
        public async Task<IEnumerable<Message>> GetAllMessageByConvId(int convId)
        {
            return await conversationDAO.GetAllMessageByConvId(convId);
        }
        public async Task<Message> AddMessage(Message item) => await conversationDAO.AddMessage(item);
    }
}
