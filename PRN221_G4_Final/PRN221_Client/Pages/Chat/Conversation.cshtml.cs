using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using PRN221_BusinessLogic.Interface;
using PRN221_Client.Hubs;
using PRN221_Client.ViewModel;
using PRN221_Models.Models;

namespace PRN221_Client.Pages.Chat
{
    public class ConversationModel : PageModel
    {
        private readonly IConversService _conversService;
        private readonly IHubContext<SignalRServer> _hubContext;

        public ConversationModel(IConversService conversService, IHubContext<SignalRServer> hubContext)
        {
            _conversService = conversService;
            _hubContext = hubContext;
        }

        public IEnumerable<Conversation> ConversList { get; set; }
        public IEnumerable<Message> MessageList { get; set; }
        public void OnGet()
        {
        }

        public async Task<PartialViewResult> OnGetLoadConversationList(int accId)
        {
            var listConvers = await _conversService.GetAllConversationByAccId(accId);

            ConversList = listConvers;

            return Partial("_ListConversation", ConversList);
        }

        public async Task<PartialViewResult> OnGetLoadMessageList(int convId)
        {

            var listMessages = await _conversService.GetAllMessageByConvId(convId);

            //MessageList = listMessages;
            var viewModel = new MessageListViewModel
            {
                Messages = listMessages,
                ConversationId = convId
            };

            await _hubContext.Clients.All.SendAsync("LoadMessages");

            return Partial("_ListMessage", viewModel);
        }

        public async Task<PartialViewResult> OnGetAddMessage(int senderId, int convId, string content)
        {
            var newMessage = new PRN221_Models.Models.Message
            {
                MessageId = 0,
                SenderId = senderId,
                Content = content,
                ConversationId = convId,
                CreateAt = DateTime.Now,
                IsDeleted = false
            };

            await _conversService.AddMessage(newMessage);

            var listMessages = await _conversService.GetAllMessageByConvId(convId);

            //MessageList = listMessages;
            var viewModel = new MessageListViewModel
            {
                Messages = listMessages,
                ConversationId = convId
            };

            await _hubContext.Clients.All.SendAsync("LoadMessages");

            return Partial("_ListMessage", viewModel);
        }
    }
}
