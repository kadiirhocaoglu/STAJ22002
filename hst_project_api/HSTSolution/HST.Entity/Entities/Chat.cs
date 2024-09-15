using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HST.Entity.Entities
{
    public sealed class Chat
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ToUserId {  get; set; }
        public string Message { get; set; } 
        public DateTime Date { get; set; } 

    }
}
