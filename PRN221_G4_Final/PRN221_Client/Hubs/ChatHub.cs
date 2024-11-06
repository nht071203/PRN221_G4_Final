using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.SignalR;
using PRN221_BusinessLogic.Interface;
using System.Collections.Concurrent;

namespace PRN221_Client.Hubs
{
    public class ChatHub : Hub
    {
        private static readonly ConcurrentDictionary<string, string> _userConnection = new ConcurrentDictionary<string, string>();
        private readonly IConversService _conversService;

        public ChatHub(IConversService conversService)
        {
            _conversService = conversService;
        }

        /* public override Task OnConnectedAsync()
         {
             string userId = Context.GetHttpContext()?.Request.Query["user_id"];
             if (!string.IsNullOrEmpty(userId))
             {
                 _userConnection[userId] = Context.ConnectionId;
             }
             return base.OnConnectedAsync();
         }*/

        //public override Task OnConnectedAsync()
        //{
        //    string userId = Context.GetHttpContext()?.Request.Query["user_id"];
        //    Console.WriteLine($"User connected with ID: {userId} and ConnectionId: {Context.ConnectionId}"); // Thêm log
        //    if (!string.IsNullOrEmpty(userId))
        //    {
        //        _userConnection[userId] = Context.ConnectionId;
        //    }
        //    return base.OnConnectedAsync();
        //}


        //public override Task OnDisconnectedAsync(Exception? exception)
        //{
        //    string connectionId = Context.ConnectionId;
        //    var user = _userConnection.FirstOrDefault(u => u.Value == connectionId).Key;
        //    if (user != null)
        //    {
        //        _userConnection.TryRemove(user, out _);
        //    }
        //    return base.OnDisconnectedAsync(exception);
        //}

        public override async Task OnConnectedAsync()
        {
            // Khi kết nối, thêm người dùng vào phòng dựa trên conversationId
            string conversationId = Context.GetHttpContext().Request.Query["conversationId"];
            await Groups.AddToGroupAsync(Context.ConnectionId, conversationId);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string conversationId = Context.GetHttpContext().Request.Query["conversationId"];
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, conversationId);

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string fromUserId, string toUserId, string messageContent, string conversationId)
        {
            Console.WriteLine("Gui tin nhan");
            // Lưu tin nhắn vào cơ sở dữ liệu
            var newMessage = new PRN221_Models.Models.Message
            {
                MessageId = 0,
                ConversationId = int.Parse(conversationId),
                SenderId = int.Parse(fromUserId), // Giả sử `fromUser` là ID của người gửi
                Content = messageContent,
                CreateAt = DateTime.Now,
                IsDeleted = false,
            };

            await _conversService.AddMessage(newMessage);

            await Clients.Group(conversationId.ToString()).SendAsync("ReceiveMessage", fromUserId, messageContent);

        }
    }
}
