using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HST.DTO.DTOs.ChatDtos
{
    public class ChatDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ToUserId { get; set; }
        public string Message { get; set; }
        public DateTime Date = DateTime.Now;
    }
}
