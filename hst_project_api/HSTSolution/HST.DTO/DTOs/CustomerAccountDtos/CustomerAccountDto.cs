using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HST.DTO.DTOs.CustomerAccountDtos
{
    public class CustomerAccountDto
    {
        public int Id { get; set; }
        public string CustomerAccountNumber { get; set; }
        public string CustomerAccountCurrency { get; set; }
        public decimal CustomerAccountBalance { get; set; }
        public int AppUserID { get; set; }
    }
}
