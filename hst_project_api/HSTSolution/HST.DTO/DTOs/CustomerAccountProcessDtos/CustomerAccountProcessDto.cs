using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HST.DTO.DTOs.CustomerAccountProcessDtos
{
    public class CustomerAccountProcessDto
    {
        public int Id { get; set; }
        public DateTime ProcessDate { get; set; } = DateTime.Now;
        public string PayerEmail { get; set; }
        public string PayerPhoneNumber { get; set; }
        public ProcessStatusEnum ProcessStatusEnum { get; set; }
        public ProcessTypeEnum ProcessTypeEnum { get; set; }
        public decimal Amount { get; set; }

        //   public int? ReceiverID { get; set; }
        // public CustomerAccount ReceiverCustomer { get; set; }
        public string Description { get; set; }
    }
}
