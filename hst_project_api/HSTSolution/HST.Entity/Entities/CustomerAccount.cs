

namespace HST.Entity.Entities
{
    public class CustomerAccount
    {
        public int Id { get; set; }
        public string CustomerAccountNumber { get; set; }
        public string CustomerAccountCurrency { get; set; }
        public decimal CustomerAccountBalance { get; set; }
        public int AppUserID { get; set; }
        public AppUser AppUser { get; set; }
        public List<CustomerAccountProcess> CustomerAccountProcesses { get; set; }
       
    }
}
