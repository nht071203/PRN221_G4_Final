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

        public override Task OnConnectedAsync()
        {
            string userId = Context.GetHttpContext()?.Request.Query["user_id"];
            Console.WriteLine($"User connected with ID: {userId} and ConnectionId: {Context.ConnectionId}"); // Thêm log
            if (!string.IsNullOrEmpty(userId))
            {
                _userConnection[userId] = Context.ConnectionId;
            }
            return base.OnConnectedAsync();
        }


        public override Task OnDisconnectedAsync(Exception? exception)
        {
            string connectionId = Context.ConnectionId;
            var user = _userConnection.FirstOrDefault(u => u.Value == connectionId).Key;
            if (user != null)
            {
                _userConnection.TryRemove(user, out _);
            }
            return base.OnDisconnectedAsync(exception);
        }

        /*public async Task SendPrivateMessage(string toUser, string message, string fromUser, int conversationId)
        {
            Console.WriteLine("Luu tin nhan");
            if (_userConnection.TryGetValue(toUser, out string connectionId))
            {
                // Lưu tin nhắn vào database
                var newMessage = new PRN221_Models.Models.Message
                {
                    ConversationId = conversationId,
                    SenderId = int.Parse(fromUser), // Giả sử `fromUser` là ID của người gửi
                    Content = message,
                    CreateAt = DateTime.Now,
                    IsDeleted = false,
                };

                await _conversService.AddMessage(newMessage);

                // Gửi tin nhắn cho người nhận
                await Clients.Client(connectionId).SendAsync("ReceiveMessage", fromUser, message);
            }
        }*/
        public async Task SendPrivateMessage(string toUser, string message, string fromUser, string conversationId)
        {
            try
            {
                Console.WriteLine("Lưu tin nhắn");
                if (_userConnection.TryGetValue(toUser, out string connectionId))
                {
                    /*// Lưu tin nhắn vào database
                    var newMessage = new PRN221_Models.Models.Message
                    {
                        ConversationId = int.Parse(conversationId),
                        SenderId = int.Parse(fromUser), // Giả sử `fromUser` là ID của người gửi
                        Content = message,
                        CreateAt = DateTime.Now,
                        IsDeleted = false,  
                    };

                    await _conversService.AddMessage(newMessage);*/

                    // Gửi tin nhắn cho người nhận
                    await Clients.Client(connectionId).SendAsync("ReceiveMessage", fromUser, message);
                }
                else
                {
                    Console.WriteLine($"Không tìm thấy kết nối cho người dùng: {toUser}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi gửi tin nhắn riêng: {ex.Message}");
                throw;
            }
        }

    }
}
