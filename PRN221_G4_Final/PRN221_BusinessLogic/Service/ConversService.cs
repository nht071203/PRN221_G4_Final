using PRN221_BusinessLogic.Interface;
using PRN221_Models.Models;
using PRN221_Repository.ConversRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_BusinessLogic.Service
{
    public class ConversService : IConversService
    {
        private IConversRepository _conversService;

        public ConversService(IConversRepository conversService)
        {
            _conversService = conversService;
        }
        public async Task<IEnumerable<Conversation>> GetAllConversationByAccId(int accId)
        {
            return await _conversService.GetAllConversationByAccId(accId);
        }
        public async Task<AccountConversation> GetAccConversationByAccOId(int conversId, int accId)
        {
            return await _conversService.GetAccConversationByAccOId(conversId, accId);
        }
        public async Task<IEnumerable<Message>> GetAllMessageByConvId(int convId)
        {
            return await _conversService.GetAllMessageByConvId(convId);
        }
        public async Task<Message> AddMessage(Message item)
        {
            return await _conversService.AddMessage(item);
        }
    }
}
