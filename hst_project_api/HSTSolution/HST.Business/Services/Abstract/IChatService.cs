using System.Collections.Generic;
using System.Threading.Tasks;
using HST.DTO.DTOs.ChatDtos;
using HST.Entity.Entities;

namespace HST.Business.Services.Abstract
{
    public interface IChatService
    {
        Task<List<Chat>> GetChatsAsync(int userId, int toUserId, CancellationToken cancellationToken);
        Task<Chat> SendMessageAsync(SendChatDto request, CancellationToken cancellationToken);                    
    }
}
