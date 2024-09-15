using HST.Business.Services.Abstract;
using HST.DTO.DTOs.ChatDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HST.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService ?? throw new ArgumentNullException(nameof(chatService));
        }

        [HttpGet("chats")]
        public async Task<IActionResult> GetChats([FromQuery] int userId, [FromQuery] int toUserId, CancellationToken cancellationToken)
        {
            try
            {
                var chats = await _chatService.GetChatsAsync(userId, toUserId, cancellationToken);
                return Ok(chats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching chats: {ex.Message}");
            }
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] SendChatDto request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("Request cannot be null.");
                }

                var chat = await _chatService.SendMessageAsync(request, cancellationToken);
                return Ok(chat);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while sending the message: {ex.Message}");
            }
        }
    }
}
