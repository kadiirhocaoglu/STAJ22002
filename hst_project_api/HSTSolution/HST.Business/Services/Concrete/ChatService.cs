using HST.Business.Hubs;
using HST.Business.Services.Abstract;
using HST.DataAccess.UnitOfWork.Abstact;
using HST.DTO.DTOs.ChatDtos;
using HST.Entity.Entities;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HST.Business.Services.Concrete
{
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IUserService _userService;
        public ChatService(IUnitOfWork unitOfWork, IHubContext<ChatHub> hubContext, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
            _userService = userService;
        }

        public async Task<List<Chat>> GetChatsAsync(int userId, int toUserId, CancellationToken cancellationToken)
        {
            var chatRepository = _unitOfWork.GetRepository<Chat>();
            var chats = await chatRepository.GetAllAsync(
                p => (p.UserId == userId && p.ToUserId == toUserId) || (p.ToUserId == userId && p.UserId == toUserId)
            );
            return chats.OrderBy(p => p.Date).ToList();
        }

        public async Task<Chat> SendMessageAsync(SendChatDto request, CancellationToken cancellationToken)
        {
            var chatRepository = _unitOfWork.GetRepository<Chat>();

            var chat = new Chat
            {
                UserId = request.UserId,
                ToUserId = request.ToUserId,
                Message = request.Message,
                Date = DateTime.Now
            };

            await chatRepository.AddAsync(chat);
            await _unitOfWork.SaveAsync();


            

            string connectionId = ChatHub.Users.FirstOrDefault(p => p.Value == request.ToUserId).Key;

            if (connectionId != null)
            {
                await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveMessage", chat);
            }

            return chat;
        }
    }
}
