
namespace HST.Entity.Entities
{
    public class CustomerAccountProcess
    {
        public int Id { get; set; }
        public DateTime ProcessDate { get; set; } = DateTime.Now;
        public string PayerEmail { get; set; }
        public string PayerPhoneNumber { get; set; }
        public ProcessStatusEnum ProcessStatusEnum { get; set; }
        public ProcessTypeEnum ProcessTypeEnum { get; set; }
        public decimal Amount { get; set; }
        public int? ReceiverID { get; set; }
        public CustomerAccount ReceiverCustomer { get; set; }
        public string Description { get; set; }
    }
}
