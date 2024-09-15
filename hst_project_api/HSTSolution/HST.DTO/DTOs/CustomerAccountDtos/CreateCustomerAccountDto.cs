using System;

namespace HST.DTO.DTOs.CustomerAccountDtos
{
    public class CreateCustomerAccountDto
    {
        public string CustomerAccountNumber { get; private set; }
        public string CustomerAccountCurrency { get; private set; }
        public decimal CustomerAccountBalance { get; set; } = 0;
        public int AppUserID { get; set; }

        public CreateCustomerAccountDto()
        {
            CustomerAccountNumber = GenerateRandomAccountNumber();
            CustomerAccountCurrency = "TL";
        }

        private string GenerateRandomAccountNumber()
        {
            Random random = new Random();
            return random.Next(10000000, 99999999).ToString();
        }
    }
}
