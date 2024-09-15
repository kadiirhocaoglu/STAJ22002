using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HST.DTO.DTOs.ChatDtos
{
    public class SendChatDto
    {
        public int UserId { get; set; }
        public int ToUserId { get; set; }
        public string Message { get; set; }
    }
}
