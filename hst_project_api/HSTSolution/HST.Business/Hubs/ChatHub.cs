using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using HST.Entity.Entities;
using HST.DataAccess.UnitOfWork.Abstact;

namespace HST.Business.Hubs
{
    public sealed class ChatHub : Hub
    {
        private readonly IUnitOfWork _unitOfWork;
        public static readonly ConcurrentDictionary<string, int> Users = new();

        public ChatHub(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task Connect(int userId)
        {
            if (string.IsNullOrEmpty(Context.ConnectionId))
            {
                await Clients.Caller.SendAsync("Error", "Connection ID is null or empty.");
                return;
            }

            if (Users.TryAdd(Context.ConnectionId, userId))
            {
                Console.WriteLine($"New connection established. Connection ID: {Context.ConnectionId}");

                var user = await _unitOfWork.GetRepository<AppUser>().GetByIdAsync(userId);
                if (user != null)
                {
                    user.IsLiveSupport = true;
                    await _unitOfWork.SaveAsync();

                    var newUser = new { Id = user.Id, Name = user.UserName };
                    await Clients.All.SendAsync("NewUserConnected", newUser);

                    await Clients.Caller.SendAsync("Connected", "Connected to customer service.");
                }
                else
                {
                    await Clients.Caller.SendAsync("Error", "User not found.");
                }
            }
            else
            {
                await Clients.Caller.SendAsync("Error", "Connection ID already in use.");
            }
        }


        public async Task SendMessage(string message, int toUserId)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                await Clients.Caller.SendAsync("Error", "Message cannot be empty.");
                return;
            }

            if (Users.TryGetValue(Context.ConnectionId, out int fromUserId))
            {
                var chat = new Chat
                {
                    UserId = fromUserId,
                    ToUserId = toUserId,
                    Message = message,
                    Date = DateTime.Now
                };

                try
                {
                    await _unitOfWork.GetRepository<Chat>().AddAsync(chat);
                    await _unitOfWork.SaveAsync();

                    var connectionId = Users.FirstOrDefault(p => p.Value == toUserId).Key;
                    if (connectionId != null)
                    {
                        await Clients.Client(connectionId).SendAsync("ReceiveMessage", fromUserId, message);
                    }
                    else
                    {
                        await Clients.Caller.SendAsync("Error", "Recipient is not connected.");
                    }
                }
                catch (Exception ex)
                {
                    await Clients.Caller.SendAsync("Error", "An error occurred while sending the message.");
                    Console.WriteLine($"Exception in SendMessage method: {ex.Message}\n{ex.StackTrace}");
                }
            }
            else
            {
                await Clients.Caller.SendAsync("Error", "Connection ID not recognized.");
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            try
            {
                if (Users.TryRemove(Context.ConnectionId, out int userId))
                {
                    
                    Console.WriteLine($"Connection disconnected. Connection ID: {Context.ConnectionId}");
                    await Clients.All.SendAsync("UserDisconnected", userId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in OnDisconnectedAsync method: {ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                await base.OnDisconnectedAsync(exception);
            }
        }
    }
}
