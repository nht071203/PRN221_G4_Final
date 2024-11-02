using Microsoft.EntityFrameworkCore;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_DataAccess.DAOs
{
    public class ConversationDAO : SingletonBase<ConversationDAO>
    {
        public async Task<IEnumerable<Conversation>> GetAllConversationByAccId(int accId)
        {
            return await (from c in _context.Conversations
                          join ac in _context.AccountConversations on c.ConversationId equals ac.ConversationId
                          where ac.AccountId == accId
                                && ac.IsOut == false
                                && c.IsDeleted == false
                          select c).ToListAsync();
        }

        public async Task<AccountConversation> GetAccConversationByAccOId(int convId, int accId)
        {
            var item = await _context.AccountConversations.FirstOrDefaultAsync(c => c.ConversationId == convId && c.AccountId != accId);
            if (item == null) return null;
            return item;
        }

        public async Task<IEnumerable<Message>> GetAllMessageByConvId(int conv)
        {
            return await _context.Messages.Where(m => m.ConversationId == conv).ToListAsync();
        }

        public async Task<Message> AddMessage(Message item)
        {
            _context.Messages.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
